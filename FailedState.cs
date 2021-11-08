using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    class FailedState : GameState
    {
        private double backgroundX, backgroundY;
        private int changeDirection;

        private Bitmap backgroundBitmap;
        private Bitmap mainBitmap;
        private Bitmap failedText;

        private AnimationScript mainAnimScript;
        private Animation mainAnimation;
        private DrawingOptions mainOpt;

        public override void OnStateEnter()
        {
            backgroundX = 0;
            backgroundY = 0;
            changeDirection = -1;

            backgroundBitmap = new Bitmap("FailedBackground", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\UI\\StartBackground.png");
            mainBitmap = new Bitmap("Player", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\Player\\Player.png");
            mainBitmap.SetCellDetails(120, 120, 5, 8, 40);
            failedText = new Bitmap("FailedText", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\UI\\LevelFailed.png");

            mainAnimScript = new AnimationScript("player", "player.txt");
            mainAnimation = SplashKit.CreateAnimation(mainAnimScript, "fall");
            mainOpt = SplashKit.OptionWithAnimation(mainAnimation);
            mainOpt = SplashKit.OptionScaleBmp(4, 4, mainOpt);
        }


        public override void CheckInput()
        {
            if (SplashKit.KeyDown(KeyCode.SpaceKey))
            {
                if (manager.CurrentStage == 1)
                    manager.SetCurrentState<StageState1>();
                if (manager.CurrentStage == 2)
                    manager.SetCurrentState<StageState2>();
            }
        }

        public override void Update()
        {
            mainAnimation.Update();

            backgroundX += 1 * changeDirection;

            if (backgroundX == -685)
                changeDirection = 1;
            if (backgroundX == 0)
                changeDirection = -1;
        }

        public override void Draw()
        {
            SplashKit.DrawBitmap(backgroundBitmap, backgroundX, backgroundY);
            SplashKit.DrawBitmap(failedText, 0, 0);
            SplashKit.DrawBitmap(mainBitmap, 540, 240, mainOpt);
        }
    }
}
