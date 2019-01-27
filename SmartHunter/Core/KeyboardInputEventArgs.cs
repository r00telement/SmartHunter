using System;
using System.Windows.Input;

namespace SmartHunter.Core
{
    public class KeyboardInputEventArgs : EventArgs
    {
        public Key Key { get; private set; }
        public bool IsDown { get; private set; }

        public KeyboardInputEventArgs(Key key, bool isDown)
        {
            Key = key;
            IsDown = isDown;
        }
    }
}
