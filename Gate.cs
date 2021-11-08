using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    class Gate
    {
        private double gateX;
        private double gateY;

        private bool isOnGround;
        private bool stageComplete;

        private Bitmap gateBitmap;
        private Bitmap terrain;

        private AnimationScript gateAnimScript;
        private Animation gateAnimation;
        private DrawingOptions gateOpt;

        Player player;

        public bool StageCompleted { get { return stageComplete; } }
        public Gate(double gateX, double gateY, Bitmap terrain, Player player)
        {
            this.gateX = gateX;
            this.gateY = gateY;

            isOnGround = false;
            stageComplete = false;

            gateBitmap = new Bitmap("CrystalGate", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\Objects\\CrystalGate.png");
            gateBitmap.SetCellDetails(96, 96, 3, 5, 15);

            this.terrain = terrain;

            gateAnimScript = new AnimationScript("crystalGate", "crystalGate.txt");
            gateAnimation = SplashKit.CreateAnimation(gateAnimScript, "close");
            gateOpt = SplashKit.OptionWithAnimation(gateAnimation);

            this.player = player;
        }

        public void CollisionUpdate()
        {
            if (SplashKit.BitmapCollision(gateBitmap,gateX, gateY, terrain, 0, 0))
            {
                if (!isOnGround)
                    isOnGround = true;
            }
            else
            {
                if (isOnGround)
                    isOnGround = false;

                gateY += 5;
            }
        }

        public void CheckGateOpen()
        {
            if (player.HasKey)
            {
                if (SplashKit.AnimationName(gateAnimation) != "open")
                    gateAnimation.Assign("open");
            }
        }

        public bool StageComplete()
        {
            if (SplashKit.BitmapCollision(player.CharacterBmp, player.CharacterX, player.CharacterY, gateBitmap, gateX, gateY) && player.HasKey)
            {
                return true;
            }
            return false;
        }

        public void Update()
        {
            gateAnimation.Update();
            CollisionUpdate();
            CheckGateOpen();
            stageComplete = StageComplete();

            if (!player.HasKey && SplashKit.AnimationName(gateAnimation) != "close")
                gateAnimation.Assign("close");
        }

        public void Draw()
        {
            SplashKit.DrawBitmap(gateBitmap, gateX, gateY, gateOpt);
        }
    }
}
