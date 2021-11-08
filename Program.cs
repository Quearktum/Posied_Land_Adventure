using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    public class Program
    {
        public static void Main()
        {
            new Window("Simple Platformer", 1280, 720);

            StateManager stateManager = new StateManager();
            stateManager.AddState<StartState>();
            stateManager.AddState<CompletedState>();
            stateManager.AddState<FailedState>();
            stateManager.AddState<StageState1>();
            stateManager.AddState<StageState2>();

            stateManager.SetCurrentState<StartState>();

            while (!SplashKit.QuitRequested())
            {               
                SplashKit.ProcessEvents();

                //Update
                stateManager.CheckInput();
                stateManager.Update();
                
                SplashKit.ClearScreen();

                //Draw
                stateManager.Draw();
                //Refresh Screen
                SplashKit.RefreshScreen(60);
            }
        }
    }
}
