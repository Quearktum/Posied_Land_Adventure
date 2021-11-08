using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    class InputManager
    {
        private List<IKeyListener> observers;
        private List<KeyCode> keys;

        public InputManager()
        {
            observers = new List<IKeyListener>();

            keys = new List<KeyCode> { KeyCode.RightKey, KeyCode.LeftKey, KeyCode.UpKey, KeyCode.CKey};
        }

        public void CheckInput()
        {
            foreach(KeyCode keyCode in keys)
            {
                if (SplashKit.KeyDown(keyCode))
                    NotifyObserver(keyCode);
            }
        }

        public void NotifyObserver(KeyCode keyCode)
        {
            foreach (IKeyListener observer in observers)
                observer.OnKeyPressed(keyCode);
        }

        public void Add(IKeyListener observer)
            => observers.Add(observer);
    }
}
