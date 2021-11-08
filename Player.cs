using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    class Player : Character, IKeyListener
    {
        private int horizontalInput;
        private int playerSpeed;

        private bool isShotted;
        private bool hasKey;

        private Bitmap leftDetect;
        private Bitmap rightDetect;

        private int ammunition;
        private int jumpIndex;
        private int jumpTime;

        public bool HasKey { get { return hasKey; } set { hasKey = value; } }
        public int Ammunition { get { return ammunition; } set { ammunition = value; } }

        public Player(Bitmap terrain)
        {
            charX = 300;
            charY = 400;
            horizontalInput = 0;
            playerSpeed = 5;
            charHealth = 10;
            ammunition = 0;
            jumpIndex = 0;
            jumpTime = 10;

            isFlip = false;
            isOnGround = false;
            isShotted = false;

            leftDetect = new Bitmap("Left_dectector", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\Player\\left_detect.png");
            rightDetect = new Bitmap("Right_dectector", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\Player\\right_detect.png");
            charBitmap = new Bitmap("Player", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\Player\\Player.png");
            charBitmap.SetCellDetails(120, 120, 5, 8, 40);
            this.terrain = terrain;

            charAnimScript = new AnimationScript("player", "player.txt");
            charAnimation = SplashKit.CreateAnimation(charAnimScript, "idle");
            charOpt = SplashKit.OptionWithAnimation(charAnimation);

            charWeaponObject = new InstantiateObjects();
            charWeaponObject.SetObjectToInstan(objectToInstan.Arrow);

            hitManager = new HitManager();
            hitManager.AddDamageableType(objectToInstan.Tongue);
            hitManager.AddDamageableType(objectToInstan.Pea);
        }

        public void OnKeyPressed(KeyCode keyCode)
        {
            if(keyCode == KeyCode.LeftKey)
            {
                if (SplashKit.AnimationName(charAnimation) != "run") charAnimation.Assign("run");

                horizontalInput = -1;

                if (SplashKit.BitmapCollision(leftDetect, charX + 30, charY + 96, terrain, 0, 0))
                {
                    charX += 4;
                    charY -= 3;
                }
                else if (SplashKit.BitmapCollision(leftDetect, charX + 27, charY + 90, terrain, 0, 0)) charX += 0;
                else charX += horizontalInput * playerSpeed;
            }
            if(keyCode == KeyCode.RightKey)
            {
                if (SplashKit.AnimationName(charAnimation) != "run") charAnimation.Assign("run");

                horizontalInput = 1;

                if (SplashKit.BitmapCollision(rightDetect, charX + 90, charY + 96, terrain, 0, 0))
                {
                    charX += 4;
                    charY -= 3;
                }
                else if (SplashKit.BitmapCollision(rightDetect, charX + 93, charY + 90, terrain, 0, 0)) charX += 0;
                else charX += horizontalInput * playerSpeed;
            }

            if(keyCode == KeyCode.UpKey && isOnGround == true)
            {
                if (SplashKit.AnimationName(charAnimation) != "jump") charAnimation.Assign("jump");

                charY -= 20;
                jumpIndex += 1;
            }
            if(keyCode == KeyCode.CKey && isShotted == false && ammunition !=0)
            {
                if (SplashKit.AnimationName(charAnimation) != "shoot") charAnimation.Assign("shoot");

                isShotted = true;
                ammunition -= 1;
                charWeaponObject.Instantiate(50, 58, isFlip);           
            }
        }

        public override void CollisionUpdate()
        {
            if (SplashKit.BitmapCollision(charBitmap, charX, charY, terrain, 0, 0))
            {
                if (isOnGround != true)
                    isOnGround = true;
            }
            else
            {
                if (isOnGround != false && jumpIndex > jumpTime)
                {
                    isOnGround = false;
                    jumpIndex = 0;
                }

                charY += 5;
            }
        }

        public void FlipPlayer()
        {
            if ((horizontalInput < 0 && isFlip == false) || (horizontalInput > 0 && isFlip == true))
            {
                isFlip = !isFlip;
                charOpt = SplashKit.OptionFlipY(charOpt);
            }
        }

        public void PushBack()
        {
            if (!isFlip)
                charX -= 30;
            else if (isFlip)
                charX += 30;

            charY -= 10;
        }

        public override void Update()
        {
            if (SplashKit.KeyUp(KeyCode.CKey)) isShotted = false;

            FlipPlayer();
            CollisionUpdate();
            Fall();

            charAnimation.Update();
            horizontalInput = 0;

            //Update Hit
            if (hitManager.DealDamage(charBitmap, charX, charY) != 0)
            {
                PushBack();
                charHealth -= hitManager.CurrentDamage;
            }
            charWeaponObject.Update(charX, charY);

            if ((SplashKit.KeyUp(KeyCode.RightKey) && SplashKit.KeyUp(KeyCode.LeftKey) && SplashKit.KeyUp(KeyCode.UpKey) && SplashKit.KeyUp(KeyCode.CKey)) && SplashKit.AnimationName(charAnimation) != "idle") charAnimation.Assign("idle");
        }

        public override void Draw()
        {
            SplashKit.DrawBitmap(charBitmap, charX, charY, charOpt);
            charWeaponObject.Draw();

            SplashKit.DrawText(charHealth.ToString(), Color.White,Font.FetchOrCreate(IntPtr.Zero), 216, 0, 0, SplashKit.OptionToScreen());
            SplashKit.DrawText(ammunition.ToString(), Color.White,Font.FetchOrCreate(IntPtr.Zero), 216, 0, 18, SplashKit.OptionToScreen());
        }
    }
}
