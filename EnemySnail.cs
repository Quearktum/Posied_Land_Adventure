using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    class EnemySnail : Character
    {
        Player player;

        private Bitmap leftDetect;
        private Bitmap rightDetect;

        private int index;
        private int moveTime;

        public EnemySnail(double snailX, double snailY, Bitmap terrain, bool isFlip, Player player)
        {
            charX = snailX;
            charY = snailY;
            charHealth = 10;
            index = 0;
            moveTime = 480;

            base.isFlip = isFlip;
            isOnGround = false;
            isAttacked = false;

            leftDetect = new Bitmap("Left_dectector", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\Player\\left_detect.png");
            rightDetect = new Bitmap("Right_dectector", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\Player\\right_detect.png");
            charBitmap = new Bitmap("Snail", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\Enemies\\EnemySnail.png");
            charBitmap.SetCellDetails(72, 72, 2, 3, 6);

            triggerBox = new Rectangle();
            triggerBox.Width = 72;
            triggerBox.Height = 40;

            base.terrain = terrain;

            charAnimScript = new AnimationScript("snail", "snail.txt");
            charAnimation = SplashKit.CreateAnimation(charAnimScript, "idle");
            charOpt = SplashKit.OptionWithAnimation(charAnimation);

            if (!isFlip)
                charOpt = SplashKit.OptionFlipY(charOpt);

            this.player = player;

            charWeaponObject = new InstantiateObjects();
            charWeaponObject.SetObjectToInstan(objectToInstan.Tongue);

            hitManager = new HitManager();
            hitManager.AddDamageableType(objectToInstan.Arrow);
        }
        public bool ScanForPlayer(Player _player)
        {
            if (SplashKit.BitmapRectangleCollision(_player.CharacterBmp, _player.CharacterX, _player.CharacterY, triggerBox) && isAttacked == false)
            {
                isAttacked = true;
                if (charWeaponObject.ObjCount == 0)
                    charWeaponObject.Instantiate(-95, 0, !isFlip);
                PushBack();
                return true;
            }
            return false;
        }

        public void PushBack()
        {
            if (!isFlip)
                charX -= 10;
            else if (isFlip)
                charX += 10;

            charY -= 10;
        }

        public void MoveLeft()
        {
            if (SplashKit.BitmapCollision(leftDetect, charX - 1, charY + 40, terrain, 0, 0))
            {
                if (isFlip)
                {
                    index = 0;

                    isFlip = false;
                    charOpt = SplashKit.OptionFlipY(charOpt);
                }
            }
            else if (index < moveTime)
            {
                charX -= 1;
                index += 1;
            }
            else if(index == moveTime)
            {
                index = 0;

                if (isFlip)
                {
                    isFlip = false;
                    charOpt = SplashKit.OptionFlipY(charOpt);
                }
            }
        }

        public void MoveRight()
        {
            if (SplashKit.BitmapCollision(rightDetect, charX + 73, charY + 40, terrain, 0, 0))
            {
                if (!isFlip)
                {
                    index = 0;

                    isFlip = true;
                    charOpt = SplashKit.OptionFlipY(charOpt);
                }
            }
            else if (index < moveTime)
            {
                charX += 1;
                index += 1;
            }
            else if(index == moveTime)
            {
                index = 0;

                if (!isFlip)
                {
                    isFlip = true;
                    charOpt = SplashKit.OptionFlipY(charOpt);
                }
            }
        }

        public void Move()
        {
            if (isFlip)
                MoveLeft();
            else if (!isFlip)
                MoveRight();
        }


        public override void Update()
        {
            isAttacked = false;
            CollisionUpdate();
            Move();
            charAnimation.Update();

            //Update Trigger box position
            triggerBox.X = charX;
            triggerBox.Y = charY + 32;


            charWeaponObject.Update(charX, charY);

            //Update Hit 
            if (hitManager.DealDamage(charBitmap, charX, charY) != 0)
            {
                PushBack();
                charHealth -= hitManager.CurrentDamage;
            }

            if (!ScanForPlayer(player) && SplashKit.AnimationName(charAnimation) != "idle")
                charAnimation.Assign("idle");


        }

        public override void Draw()
        {
            SplashKit.DrawBitmap(charBitmap, charX, charY, charOpt);
        }
    }
}
