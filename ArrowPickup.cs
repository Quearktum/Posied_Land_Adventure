using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    class ArrowPickup : IPickupAble
    {
        private double arrowX;
        private double arrowY;

        private bool isOnGround;
        private bool isPickup;

        private Bitmap arrowBitmap;
        private Bitmap terrain;

        private AnimationScript arrowAnimScript;
        private Animation arrowAnimation;
        private DrawingOptions arrowOpt;

        Player player;

        public bool IsPicked { get { return isPickup; } set { isPickup = value; } }

        public ArrowPickup(double arrowX, double arrowY, Bitmap terrain, Player player)
        {
            this.arrowX = arrowX;
            this.arrowY = arrowY;

            isOnGround = false;
            isPickup = false;

            arrowBitmap = new Bitmap("ArrowPickup", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\Objects\\ArrowPickup.png");
            arrowBitmap.SetCellDetails(60, 60, 2, 2, 4);

            this.terrain = terrain;

            arrowAnimScript = new AnimationScript("arrowPickup", "arrowPickup.txt");
            arrowAnimation = SplashKit.CreateAnimation(arrowAnimScript, "idle");
            arrowOpt = SplashKit.OptionWithAnimation(arrowAnimation);

            this.player = player;
        }

        public void CollisionUpdate()
        {
            if (SplashKit.BitmapCollision(arrowBitmap, arrowX, arrowY, terrain, 0, 0))
            {
                if (!isOnGround)
                    isOnGround = true;
            }
            else
            {
                if (isOnGround)
                    isOnGround = false;

                arrowY += 5;
            }
        }

        public bool IsPickup()
        {
            if (SplashKit.BitmapCollision(player.CharacterBmp, player.CharacterX, player.CharacterY, arrowBitmap, arrowX, arrowY))
            {
                return true;
            }
            return false;
        }

        public void Update()
        {
            if (!isPickup)
            {
                CollisionUpdate();
                arrowAnimation.Update();
                isPickup = IsPickup();
            }
            else if (isPickup)
            {
                player.Ammunition += 3;
            }
        }

        public void Draw()
        {
            if (!isPickup)
                SplashKit.DrawBitmap(arrowBitmap, arrowX, arrowY, arrowOpt);
        }
    }
}
