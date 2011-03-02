using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using Heroes.Core.Battle.OtherIO;

namespace Heroes.Core.Battle.Rendering
{
    public class Controller
    {
        public Control _target;
        public Device _device;

        private TextureStore _texs;
        public Sprite _sprite;
        public Microsoft.DirectX.Direct3D.Font _font;
        private Microsoft.DirectX.Direct3D.Font _messageFont;

        private bool _deviceLost;
        private PresentParameters _presentParameters;

        private Renderer _renderer;

        private int _frameRate;
        private double _elapsedTime;
        private double _previousElapsedTime;
        private PerformanceTimer _timer;

        public Size _size;

        public Controller(Control target, Renderer renderer, 
            Size size, EventHandler progressReport)
        {
            _target = target;
            _renderer = renderer;
            _size = size;
            _deviceLost = false;
            _presentParameters = new PresentParameters();
            _timer = new PerformanceTimer();

            InitializeGraphics(progressReport);
            OnDeviceReset(_device, null);

            if (progressReport != null)
            {
                progressReport(this, EventArgs.Empty);
            }

            CreateGraphicObjects(progressReport);

            _timer.Start();
            _elapsedTime = _timer.GetTime();
            _previousElapsedTime = _elapsedTime;

            if (progressReport != null)
            {
                progressReport(this, EventArgs.Empty);
            }
        }

        #region "Initalization, Creation, and Setup Methods"
        protected void InitializeGraphics(EventHandler progressReport)
        {
            // Set up our presentation parameters as usual
            if (Heroes.Core.Battle.Properties.Settings.Default.FullScreen)
            {
                _presentParameters.Windowed = false;

                ((Form)_target).FormBorderStyle = FormBorderStyle.None;

                //_size = new Size(800, 600);
                //((Form)_target).Size = new Size(800, 600);
                //((Form)_target).ClientSize = new Size(800, 600);
                _presentParameters.BackBufferFormat = Format.X8R8G8B8; // maybe needs to be changed...
                //_presentParameters.BackBufferHeight = this._target.Size.Height;
                //_presentParameters.BackBufferWidth = this._target.Size.Width;
                _presentParameters.BackBufferHeight = _size.Height;
                _presentParameters.BackBufferWidth = _size.Width;
                _presentParameters.PresentationInterval = PresentInterval.Default;
            }
            else
            {
                _presentParameters.Windowed = true;
            }

            _presentParameters.SwapEffect = SwapEffect.Discard;
            _presentParameters.AutoDepthStencilFormat = DepthFormat.D16;
            _presentParameters.EnableAutoDepthStencil = true;

            // store our default adapter
            int adapterOrdinal = Manager.Adapters.Default.Adapter;

            // get our device capabilities so we can check
            Caps caps = Manager.GetDeviceCaps(adapterOrdinal, DeviceType.Hardware);

            CreateFlags createFlags;
            if (caps.DeviceCaps.SupportsHardwareTransformAndLight)
            {
                createFlags = CreateFlags.HardwareVertexProcessing;
            }
            else
            {
                createFlags = CreateFlags.SoftwareVertexProcessing;
            }

            if (caps.DeviceCaps.SupportsPureDevice)
            {
                createFlags = createFlags | CreateFlags.PureDevice;
            }

            if (progressReport != null)
            {
                progressReport(this, EventArgs.Empty);
            }

            // create our device
            _device = new Device(adapterOrdinal, DeviceType.Hardware, _target, createFlags, _presentParameters);

            // Hook the DeviceReset event so OnDeviceReset will get called every
            // time we call device.Reset()
            _device.DeviceReset += new EventHandler(OnDeviceReset);

            // Similarly, OnDeviceLost will get called every time we call 
            // device.Reset(). The difference is that DeviceLost gets called
            // earlier, giving us a chance to do the cleanup that needs to 
            // occur before we can call Reset() successfully
            _device.DeviceLost += new EventHandler(OnDeviceLost);

            if (progressReport != null)
            {
                progressReport(this, EventArgs.Empty);
            }

            SetupDevice();
        }

        protected void SetupDevice()
        {
            _device.RenderState.AlphaBlendEnable = true;

            _device.SetSamplerState(0, SamplerStageStates.MinFilter, (int)(TextureFilter.Linear));
            _device.SetSamplerState(0, SamplerStageStates.MagFilter, (int)(TextureFilter.Linear));
            _device.SetSamplerState(0, SamplerStageStates.MipFilter, (int)(TextureFilter.Linear));

            _device.RenderState.Lighting = false;

            // get camera vectors
            Single width = (Single)(_target.Size.Width);
            Single height = (Single)(_target.Size.Height);
            Single centerX = width / 2.0F;
            Single centerY = height / 2.0F;

            Vector3 cameraPosition = new Vector3(centerX, centerY, -5.0F);
            Vector3 cameraTarget = new Vector3(centerX, centerY, 0.0F);

            // create our transforms
            _device.Transform.View = Matrix.LookAtLH(cameraPosition, cameraTarget, new Vector3(0.0F, 1.0F, 0.0F));

            _device.Transform.Projection = Matrix.OrthoLH(width, height, 1.0F, 10.0F);
        }
        #endregion

        #region "Creates Graphic Textures, Sprite, Font..."
        protected void CreateGraphicObjects(EventHandler progressReport)
        {
            SetupTextures(progressReport);
            _sprite = new Sprite(_device);

            _font = new Microsoft.DirectX.Direct3D.Font(_device, new System.Drawing.Font(FontFamily.GenericSerif, 11));
            FontDescription fd = new Microsoft.DirectX.Direct3D.FontDescription();
            fd.FaceName = "Franklin Gothic Demi";
            fd.PitchAndFamily = PitchAndFamily.DefaultPitch;
            fd.Weight = FontWeight.Thin;
            fd.Height = 20;
            fd.Quality = FontQuality.AntiAliased;
            _messageFont = new Microsoft.DirectX.Direct3D.Font(_device, fd);
        }

        public void SetupTextures()
        {
            SetupTextures(null);
        }

        public void SetupTextures(EventHandler progressReport)
        {
            if (_texs != null)
            {
                _texs.LoadTextures(_device, progressReport);
            }
            else
            {
                _texs = new TextureStore(_device, progressReport);
            }
        }
        #endregion

        #region "Device Event Handlers"
        /// <summary>
        /// Called to recreate the device when it is lost.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnDeviceReset(object sender, EventArgs e)
        {
            // We use the same setup code to reset as we do for initial creation
            SetupDevice();
        }

        protected void OnDeviceLost(object sender, EventArgs e)
        {
            // Clean up the VertexBuffer			
        }
        #endregion

        /// <summary>
        /// Main method of the controller, renders a scene by calling on the 
        /// current Renderer object.  Also proceses input events and 
        /// calculates the frame rate.
        /// </summary>
        public void Render()
        {
            CalculateTimeDependentInformation();

            if (_deviceLost)
            {
                // Try to get the device back
                AttemptRecovery();
            }

            // If we couldn't get the device back, don't try to render
            if (_deviceLost)
            {
                return;
            }

            _device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, System.Drawing.Color.FromArgb(100, 255, 255, 255), 1.0F, 0);
            _device.BeginScene();


            _sprite.Begin(SpriteFlags.AlphaBlend);

            _renderer.Render(this);

            _sprite.End();


            _device.EndScene();


            try
            {
                // Copy the back buffer to the display
                _device.Present();
            }
            catch (DeviceLostException e1)
            {
                // Indicate that the device has been lost
                _deviceLost = true;

                // Spew a message into the output window of the debugger
                Debug.WriteLine("Device was lost. {0}", e1.Message);
            }
        }

        protected void AttemptRecovery()
        {
            try
            {
                _device.TestCooperativeLevel();
            }
            catch (DeviceLostException e1)
            {
            }
            catch (DeviceNotResetException e2)
            {
                try
                {
                    _device.Reset(_presentParameters);
                    _deviceLost = false;

                    // Spew a message into the output window of the debugger
                    Debug.WriteLine("Device successfully reset");
                }
                catch (DeviceLostException e3)
                {
                    // If it's still lost or lost again, just do 
                    // nothing
                }
            }
        }

        public void DisposeGraphics()
        {
            if (this._sprite != null)
                this._sprite.Dispose();

            if (this._texs != null)
                this._texs.Dispose();

            if (this._device != null)
                this._device.Dispose();
        }

        /// <summary>
        /// Calculates the frame rate in a hokey way.
        /// </summary>
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

        #region Properties
        public Device Device
        {
            get { return _device; }
        }
        public Renderer Renderer
        {
            get { return _renderer; }
            set { _renderer = value; }
        }
        public TextureStore TextureStore
        {
            get { return _texs; }
        }
        public Sprite Sprite
        {
            get { return _sprite; }
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
        public long LastTick
        {
            get { return _timer.CurrentTickCount; }
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
        #endregion

    }
}
