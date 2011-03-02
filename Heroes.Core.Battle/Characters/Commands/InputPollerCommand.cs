using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.DirectInput;

using Heroes.Core.Battle.OtherIO;

namespace Heroes.Core.Battle.Characters.Commands
{
    public class InputPollerCommand : InputCommand, IDisposable
    {
        Poller _poller;

        public InputPollerCommand(Poller poller)
        {
            _poller = poller;
            _poller.MouseAction += new Poller.MouseActionEventHandler(_poller_MouseAction);
        }

        void _poller_MouseAction(Microsoft.DirectX.DirectInput.MouseState mouse)
        {
            _dx = mouse.X;
            _dy = mouse.Y;
            _buttons = mouse.GetMouseButtons();
        }

        public void GetInput()
        {
            _poller.Poll();
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_poller != null)
            {
                _poller.MouseAction -= _poller_MouseAction;
                _poller = null;
            }
            //If OutputRecordStream IsNot Nothing Then
            //    OutputRecordStream.Close()
            //    OutputRecordStream = Nothing
            //End If
        }

        #endregion
    }
}
