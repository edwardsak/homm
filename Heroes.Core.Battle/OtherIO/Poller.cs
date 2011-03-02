using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX.DirectInput;

namespace Heroes.Core.Battle.OtherIO
{
    /// <summary>
	/// Summary description for Poller.
	/// </summary>
    public class Poller
    {
        #region Events
        public delegate void KeysPressedEventHandler(Key[] keys);
        public delegate void MouseActionEventHandler(MouseState mouse);

        public event KeysPressedEventHandler KeysPressed;
        public event MouseActionEventHandler MouseAction;

        protected virtual void OnKeysPressed(Key[] keys)
        {
            if (KeysPressed != null)
            {
                //Invokes the delegates.
                KeysPressed(keys);
            }
        }

        protected virtual void OnMouseAction(MouseState mouse)
        {
            if (MouseAction != null)
            {
                //Invokes the delegates.
                MouseAction(mouse);
            }
        }
        #endregion

        private Device mouse;
        private Device keyboard;

        public Poller(Control target)
        {
            CreateInputDevices(target);
        }

        protected void CreateInputDevices(Control target)
        {
            // create keyboard device.
            keyboard = new Device(SystemGuid.Keyboard);
            if (keyboard == null)
            {
                throw new Exception("No keyboard found.");
            }

            // create mouse device.
            mouse = new Device(SystemGuid.Mouse);
            if (mouse == null)
            {
                throw new Exception("No mouse found.");
            }

            // set cooperative level.
            keyboard.SetCooperativeLevel(target, CooperativeLevelFlags.NonExclusive | CooperativeLevelFlags.Background);

            mouse.SetCooperativeLevel(target, CooperativeLevelFlags.NonExclusive | CooperativeLevelFlags.Background);

            // Acquire devices for capturing.
            keyboard.Acquire();
            mouse.Acquire();
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public void Poll()
        {
            if (KeysPressed != null)
            {
                Key[] keys = keyboard.GetPressedKeys();
                OnKeysPressed(keys);
            }

            OnMouseAction(mouse.CurrentMouseState);
        }

    }
}
