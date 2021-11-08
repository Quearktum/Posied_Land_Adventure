using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    class KeyPickup : IPickupAble
    {
        private double keyX;
        private double keyY;

        private bool isOnGround;
        private bool isPickup;

        private Bitmap keyBitmap;
        private Bitmap terrain;

        private AnimationScript keyAnimScript;
        private Animation keyAnimation;
        private DrawingOptions keyOpt;

        Player player;

        public bool IsPicked { get { return isPickup; } set { isPickup = value; } }

        public KeyPickup(double keyX, double keyY, Bitmap terrain, Player player)
        {
            this.keyX = keyX;
            this.keyY = keyY;

            isOnGround = false;
            isPickup = false;

            keyBitmap = new Bitmap("KeyPickup", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\Objects\\KeyPickup.png");
            keyBitmap.SetCellDetails(33, 39, 2, 2, 4);

            this.terrain = terrain;

            keyAnimScript = new AnimationScript("keyPickup", "keyPickup.txt");
            keyAnimation = SplashKit.CreateAnimation(keyAnimScript, "idle");
            keyOpt = SplashKit.OptionWithAnimation(keyAnimation);

            this.player = player;
        }

        public void CollisionUpdate()
        {
            if (SplashKit.BitmapCollision(keyBitmap, keyX, keyY, terrain, 0, 0))
            {
                if (!isOnGround)
                    isOnGround = true;
            }
            else
            {
                if (isOnGround)
                    isOnGround = false;

                keyY += 5;
            }
        }

        public bool IsPickup()
        {
            if(SplashKit.BitmapCollision(player.CharacterBmp, player.CharacterX, player.CharacterY, keyBitmap, keyX, keyY))
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
                keyAnimation.Update();
                isPickup = IsPickup();
            }
            else if (isPickup)
            {
                player.HasKey = true;
            }
        }

        public void Draw()
        {
            if (!isPickup)
                SplashKit.DrawBitmap(keyBitmap, keyX, keyY, keyOpt);
        }
    }
}
