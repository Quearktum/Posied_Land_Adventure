using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    class StartState : GameState
    {
        private double backgroundX, backgroundY;
        private int changeDirection;

        private Bitmap backgroundBitmap;
        private Bitmap mainBitmap;
        private Bitmap gameTitle;
        private Bitmap pressToStart;

        private AnimationScript mainAnimScript;
        private Animation mainAnimation;
        private DrawingOptions mainOpt;

        private AnimationScript pressAnimScript;
        private Animation pressAnimation;
        private DrawingOptions pressOpt;

        public override void OnStateEnter() 
        {
            backgroundX = 0;
            backgroundY = 0;
            changeDirection = -1;

            backgroundBitmap = new Bitmap("StartBackground", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\UI\\StartBackground.png");
            mainBitmap = new Bitmap("Player", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\Player\\Player.png");
            mainBitmap.SetCellDetails(120, 120, 5, 8, 40);
            gameTitle = new Bitmap("GameTitle", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\UI\\GameTitle.png");
            pressToStart = new Bitmap("pressToStart", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\UI\\PressToStart.png");
            pressToStart.SetCellDetails(1280, 720, 2, 2, 4);

            mainAnimScript = new AnimationScript("player", "player.txt");
            mainAnimation = SplashKit.CreateAnimation(mainAnimScript, "idle");
            mainOpt = SplashKit.OptionWithAnimation(mainAnimation);
            mainOpt = SplashKit.OptionScaleBmp(4, 4, mainOpt);

            pressAnimScript = new AnimationScript("PressToStart", "pressToStart.txt");
            pressAnimation = SplashKit.CreateAnimation(pressAnimScript, "idle");
            pressOpt = SplashKit.OptionWithAnimation(pressAnimation);
        }


        public override void CheckInput()
        {
            if(SplashKit.KeyDown(KeyCode.CKey) && manager.CurrentStage == 1)
                manager.SetCurrentState<StageState1>();
        }

        public override void Update()
        {
            if (manager.CurrentStage != 1)
                manager.CurrentStage = 1;

            mainAnimation.Update();
            pressAnimation.Update();

            backgroundX += 1 * changeDirection;

            if (backgroundX == -685)
                changeDirection = 1;
            if (backgroundX == 0)
                changeDirection = -1;
        }

        public override void Draw()
        {
            SplashKit.DrawBitmap(backgroundBitmap, backgroundX, backgroundY);
            SplashKit.DrawBitmap(gameTitle, 0, 0);
            SplashKit.DrawBitmap(pressToStart, 0, 0, pressOpt);
            SplashKit.DrawBitmap(mainBitmap, 540, 240, mainOpt);
        }
    }
}
