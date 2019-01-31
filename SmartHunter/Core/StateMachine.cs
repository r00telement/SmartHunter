using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SmartHunter.Core
{
    public class StateMachine<T>
    {
        public class Transition
        {
            public T State { get; private set; }
            public Func<bool> Condition { get; private set; }
            public Action Begin { get; private set; }

            public Transition(T state, Func<bool> condition, Action begin)
            {
                State = state;
                Condition = condition;
                Begin = begin;
            }
        }

        public class StateData
        {
            public Action Update { get; private set; }
            public Transition[] Transitions { get; private set; }

            public StateData(Action update, Transition[] transitions)
            {
                Update = update;
                Transitions = transitions;
            }
        }

        Dictionary<T, StateData> m_States;
        Stopwatch m_Stopwatch;

        public T State { get; private set; }

        public StateMachine()
        {
            m_States = new Dictionary<T, StateData>();
            m_Stopwatch = new Stopwatch();
            m_Stopwatch.Start();
        }

        public void Add(T state, StateData data)
        {
            m_States[state] = data;
        }

        public void Update()
        {
            if (m_States.TryGetValue(State, out var data))
            {
                bool hasTransitioned = false;

                if (data.Transitions != null)
                {
                    foreach (var transition in data.Transitions)
                    {
                        if (transition.Condition())
                        {
                            m_Stopwatch.Stop();
                            Log.WriteLine($"State Machine: {State.ToString()} ({m_Stopwatch.ElapsedMilliseconds} ms) > {transition.State.ToString()}");
                            m_Stopwatch.Restart();

                            State = transition.State;
                            transition.Begin?.Invoke();
                            hasTransitioned = true;

                            break;
                        }
                    }
                }

                if (!hasTransitioned)
                {
                    data.Update?.Invoke();
                }
            }
        }
    }
}