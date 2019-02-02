using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Threading;

namespace SmartHunter.Core
{
    public abstract class MemoryUpdater
    {
        enum State
        {
            None,
            WaitingForProcess,
            ProcessFound,
            PatternScanning,
            PatternScanFailed,
            Working
        }

        StateMachine<State> m_StateMachine;
        List<ThreadedMemoryScan> m_MemoryScans;
        DispatcherTimer m_DispatcherTimer;

        protected abstract string ProcessName { get; }
        protected abstract int ThreadCount { get; }
        protected abstract AddressRange ScanAddressRange { get; }
        protected abstract BytePattern[] Patterns { get; }
        protected abstract int UpdatesPerSecond { get; }

        protected Process Process { get; private set; }

        public MemoryUpdater()
        {
            CreateStateMachine();

            Initialize();

            m_DispatcherTimer = new DispatcherTimer();
            m_DispatcherTimer.Tick += Update;
            TryUpdateTimerInterval();
            m_DispatcherTimer.Start();
        }

        void CreateStateMachine()
        {
            m_StateMachine = new StateMachine<State>();

            m_StateMachine.Add(State.None, new StateMachine<State>.StateData(
                null,
                new StateMachine<State>.Transition[]
                {
                new StateMachine<State>.Transition(
                    State.WaitingForProcess,
                    () => true,
                    () =>
                    {
                        Initialize();
                    })
                }));

            m_StateMachine.Add(State.WaitingForProcess, new StateMachine<State>.StateData(
                null,
                new StateMachine<State>.Transition[]
                {
                new StateMachine<State>.Transition(
                    State.ProcessFound,
                    () =>
                    {
                        var process = Process.GetProcessesByName(ProcessName).FirstOrDefault();
                        if (process != null && !process.HasExited)
                        {
                            Process = process;
                            return true;
                        }

                        return false;
                    },
                    null)
                }));

            m_StateMachine.Add(State.ProcessFound, new StateMachine<State>.StateData(
                null,
                new StateMachine<State>.Transition[]
                {
                new StateMachine<State>.Transition(
                    State.PatternScanning,
                    () => true,
                    () =>
                    {
                        foreach (var pattern in Patterns)
                        {
                            var memoryScan = new ThreadedMemoryScan(Process, ScanAddressRange, pattern, true, ThreadCount);
                            m_MemoryScans.Add(memoryScan);
                        }
                    })
                }));

            m_StateMachine.Add(State.PatternScanning, new StateMachine<State>.StateData(
                null,
                new StateMachine<State>.Transition[]
                {
                new StateMachine<State>.Transition(
                    State.Working,
                    () =>
                    {
                        var finishedWithResults = m_MemoryScans.Where(memoryScan => memoryScan.HasCompleted && memoryScan.Results.SelectMany(result => result.Matches).Any());
                        return finishedWithResults.Count() == m_MemoryScans.Count;
                    },
                    null),
                new StateMachine<State>.Transition(
                    State.PatternScanFailed,
                    () =>
                    {
                        var finishedWithoutResults = m_MemoryScans.Where(memoryScan => memoryScan.HasCompleted && !memoryScan.Results.SelectMany(result => result.Matches).Any());
                        return finishedWithoutResults.Any();
                    },
                    null)
                }));

            m_StateMachine.Add(State.Working, new StateMachine<State>.StateData(
                () =>
                {
                    try
                    {
                        UpdateMemory();
                    }
                    catch (Exception ex)
                    {
                        Log.WriteException(ex);
                    }
                },
                new StateMachine<State>.Transition[]
                {
                new StateMachine<State>.Transition(
                    State.WaitingForProcess,
                    () =>
                    {
                        return Process.HasExited;
                    },
                    () =>
                    {
                        Initialize();
                    })
                }));
        }

        private void Initialize()
        {
            Process = null;
            m_MemoryScans = new List<ThreadedMemoryScan>();
        }

        private void Update(object sender, EventArgs e)
        {
            try
            {
                m_StateMachine.Update();
            }
            catch (Exception ex)
            {
                m_DispatcherTimer.IsEnabled = false;
                Log.WriteException(ex);
            }
        }

        abstract protected void UpdateMemory();

        protected void TryUpdateTimerInterval()
        {
            const int max = 60;
            const int min = 1;
            int clampedUpdatesPerSecond = Math.Min(Math.Max(UpdatesPerSecond, min), max);

            int targetMilliseconds = (int)(1000f / clampedUpdatesPerSecond);
            if (m_DispatcherTimer != null && m_DispatcherTimer.Interval.TotalMilliseconds != targetMilliseconds)
            {
                m_DispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, targetMilliseconds);
            }
        }
    }
}