using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Threading;
using SmartHunter.Core.Helpers;
using SmartHunter.Game.Helpers;

namespace SmartHunter.Core
{
    public abstract class MemoryUpdater
    {
        enum State
        {
            None,
            CheckingForUpdates,
            DownloadingUpdates,
            Restarting,
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
        protected abstract BytePattern[] Patterns { get; }

        protected virtual int ThreadsPerScan { get { return 2; } }
        protected virtual int UpdatesPerSecond { get { return 20; } }
        protected virtual bool ShutdownWhenProcessExits { get { return false; } }

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
            var updater = new Updater();

            m_StateMachine = new StateMachine<State>();

            m_StateMachine.Add(State.None, new StateMachine<State>.StateData(
                null,
                new StateMachine<State>.Transition[]
                {
                    new StateMachine<State>.Transition(
                        State.CheckingForUpdates,
                        () => ConfigHelper.Main.Values.AutomaticallyCheckAndDownloadUpdates,
                        () =>
                        {
                            Log.WriteLine("Searching for updates (You can disable this feature in file 'Config.json')!");
                        }),
                    new StateMachine<State>.Transition(
                        State.WaitingForProcess,
                        () => !ConfigHelper.Main.Values.AutomaticallyCheckAndDownloadUpdates,
                        () =>
                        {
                            Initialize();
                        })
                }));

            m_StateMachine.Add(State.CheckingForUpdates, new StateMachine<State>.StateData(
                null,
                new StateMachine<State>.Transition[]
                {
                    new StateMachine<State>.Transition(
                        State.WaitingForProcess,
                        () => !updater.CheckForUpdates(),
                        () =>
                        {
                            Initialize();
                        }),
                    new StateMachine<State>.Transition(
                        State.DownloadingUpdates,
                        () => updater.CheckForUpdates(),
                        () =>
                        {
                            Log.WriteLine("Starting to download Updates!");
                        })
                }));

            m_StateMachine.Add(State.DownloadingUpdates, new StateMachine<State>.StateData(
                null,
                new StateMachine<State>.Transition[]
                {
                    new StateMachine<State>.Transition(
                        State.Restarting,
                        () => updater.DownloadUpdates(),
                        () =>
                        {
                            Log.WriteLine("Successfully downloaded all files!");
                        }),
                    new StateMachine<State>.Transition(
                        State.WaitingForProcess,
                        () => !updater.DownloadUpdates(),
                        () =>
                        {
                            Log.WriteLine("Failed to download Updates... Resuming the normal flow of the application!");
                            Initialize();
                        })
                }));

            m_StateMachine.Add(State.Restarting, new StateMachine<State>.StateData(
               null,
               new StateMachine<State>.Transition[]
               {
                    new StateMachine<State>.Transition(
                        State.Restarting,
                        () => true,
                        () =>
                        {
                            Log.WriteLine("Restarting Application!");
                            string update = ".\\SmartHunter_NEW.exe";
                            string exec = System.Reflection.Assembly.GetEntryAssembly()?.Location;
                            if (File.Exists(update) && exec != null && File.Exists(exec))
                            {
                                File.Move(exec, "SmartHunter_OLD.exe");
                                File.Move(update, "SmartHunter.exe");
                                Process.Start("SmartHunter.exe");
                            }
                            System.Environment.Exit(1);
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
                            var processes = Process.GetProcesses();
                            foreach (var p in processes)
                            {
                                try
                                {
                                    if (p != null && p.ProcessName.Equals(ProcessName) && !p.HasExited)
                                    {
                                        Process = p;
                                        return true;
                                    }
                                }
                                catch
                                {
                                    // nothing here
                                }
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
                                var memoryScan = new ThreadedMemoryScan(Process, pattern, true, ThreadsPerScan);
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
                        () =>
                        {
                            var orderedMatches = m_MemoryScans.Select(memoryScan => memoryScan.Results.Where(result => result.Matches.Any()).First().Matches.First()).OrderBy(match => match);
                            Log.WriteLine($"Match Range: {orderedMatches.First():X} - {orderedMatches.Last():X}");
                        }),
                    new StateMachine<State>.Transition(
                        State.PatternScanFailed,
                        () =>
                        {
                            var completedScans = m_MemoryScans.Where(memoryScan => memoryScan.HasCompleted);
                            if (completedScans.Count() == m_MemoryScans.Count)
                            {
                                var finishedWithoutResults = m_MemoryScans.Where(memoryScan => !memoryScan.Results.SelectMany(result => result.Matches).Any());
                                return finishedWithoutResults.Any();
                            }

                            return false;
                        },
                        () =>
                        {
                            var failedMemoryScans = m_MemoryScans.Where(memoryScan => !memoryScan.Results.SelectMany(result => result.Matches).Any());
                            string failedPatterns = String.Join("\r\n", failedMemoryScans.Select(failedMemoryScan => failedMemoryScan.Pattern.Config.String));
                            Log.WriteLine($"Failed Patterns:\r\n{failedPatterns}");
                        }),
                    new StateMachine<State>.Transition(
                        State.WaitingForProcess,
                        () =>
                        {
                            return Process.HasExited;
                        },
                        () =>
                        {
                            Initialize(true);
                        })
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
                            Initialize(true);
                        })
                }));
        }

        private void Initialize(bool processExited = false)
        {
            Process = null;

            if (m_MemoryScans != null)
            {
                foreach (var memoryScan in m_MemoryScans)
                {
                    memoryScan.TryCancel();
                }
            }

            m_MemoryScans = new List<ThreadedMemoryScan>();

            if (processExited && ShutdownWhenProcessExits)
            {
                Log.WriteLine("Process exited. Shutting down.");
                System.Windows.Application.Current.Shutdown();
            }
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
