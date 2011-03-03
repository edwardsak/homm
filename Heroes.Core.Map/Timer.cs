using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Heroes.Core.Map
{
    public enum TimerState
    {
        Stopped,
        Running
    }

    public class PerformanceTimer
    {
        [System.Security.SuppressUnmanagedCodeSecurity, DllImport("kernel32")]
        private extern static bool QueryPerformanceFrequency(ref long PerformanceFrequency);

        [System.Security.SuppressUnmanagedCodeSecurity, DllImport("kernel32")]
        private extern static bool QueryPerformanceCounter(ref long PerformanceCount);

        private long tickFreq;
        private TimerState timerState;
        private long lastTickCount;

        public PerformanceTimer()
        {
            timerState = TimerState.Stopped;

            if (QueryPerformanceFrequency(ref tickFreq) == false)
            {
                throw new ApplicationException("Failed to query for the performance frequency!");
            }
        }

        public void Start()
        {
            timerState = TimerState.Stopped;
            lastTickCount = CurrentTickCount;
            timerState = TimerState.Running;
        }

        public void Stop()
        {
            timerState = TimerState.Stopped;
        }

        public double GetElapsedTime()
        {
            if (timerState == TimerState.Stopped)
            {
                throw new ApplicationException("Timer is not running!");
            }

            long currTick = CurrentTickCount;
            double elapsed = (currTick - lastTickCount) / (double)tickFreq;
            lastTickCount = currTick;
            return elapsed;
        }

        public double GetTime()
        {
            long currTick = CurrentTickCount;
            double elapsed = (currTick) / (double)tickFreq;
            return elapsed;
        }

        public TimerState State
        {
            get
            {
                return timerState;
            }
        }

        public long TicksPerSecond
        {
            get
            {
                return tickFreq;
            }
        }

        public long CurrentTickCount
        {
            get
            {
                long tickCount = 0;
                if (QueryPerformanceCounter(ref tickCount) == false)
                {
                    throw new ApplicationException("Failed to query performance counter!");
                }
                return tickCount;
            }
        }

    }
}
