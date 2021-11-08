using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    interface IKeyListener
    {
        void OnKeyPressed(KeyCode keyCode);
    }
}
