using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Heroes.Core.Map
{
    public class Controller
    {
        public delegate void DrawDelegate();
        public DrawDelegate Draw;

        Control _target;

        private int _frameRate;
        private double _elapsedTime;
        private double _previousElapsedTime;
        private PerformanceTimer _timer;

        public Controller(Control target)
        {
            _target = target;
            _timer = new PerformanceTimer();

            _timer.Start();
            _elapsedTime = _timer.GetTime();
            _previousElapsedTime = _elapsedTime;
        }

        public PerformanceTimer Timer
        {
            get
            {
                return _timer;
            }
        }
        public int FrameRate
        {
            get { return _frameRate; }
        }

        public TimeSpan ElapsedTimeSinceLastRender
        {
            get
            {
                double temp = _elapsedTime - _previousElapsedTime; // 1. not sure this should be seoncds
                return TimeSpan.FromSeconds(temp); // 2. pretty sure we are recording elapsedTime = timeSinceLastRender
                // should be TIME SINCE START!!! same for previousElapsed Time!!!
            }
        }

        public void Render()
        {
            CalculateTimeDependentInformation();

            Draw();
        }

        protected void CalculateTimeDependentInformation()
        {
            const int ToPreventDivideByZero = 1;
            _previousElapsedTime = _elapsedTime;
            _elapsedTime = _timer.GetTime();

            if ((TimeSpan.FromSeconds(_elapsedTime).Milliseconds % 100) < 10)
            {
                _frameRate = (int)((TimeSpan.FromSeconds(1.0).Ticks / (TimeSpan.FromSeconds(_elapsedTime - _previousElapsedTime).Ticks + ToPreventDivideByZero)));
            }

            //m_animatedTileFrameIndex = (this.LastTick % _timer.TicksPerSecond) / (_timer.TicksPerSecond / Controller.AnimationGroupSize);
        }

    }
}
