using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    class EnemyFrog : Character
    {
        Player player;

        public EnemyFrog(double frogX, double frogY, Bitmap terrain, bool isFlip, Player player)
        {
            charX = frogX;
            charY = frogY;
            charHealth = 10;

            base.isFlip = isFlip;
            isOnGround = false;
            isAttacked = false;

            charBitmap = new Bitmap("Frog", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\Enemies\\EnemyFrog.png");
            charBitmap.SetCellDetails(72, 72, 4, 4, 36);

            triggerBox = new Rectangle();
            triggerBox.Width = 120;
            triggerBox.Height = 30;

            base.terrain = terrain;

            charAnimScript = new AnimationScript("frog", "frog.txt");
            charAnimation = SplashKit.CreateAnimation(charAnimScript, "idle");
            charOpt = SplashKit.OptionWithAnimation(charAnimation);

            if(!isFlip) charOpt = SplashKit.OptionFlipY(charOpt);

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
                if (SplashKit.AnimationName(charAnimation) != "attack") charAnimation.Assign("attack");

                isAttacked = true;
                if(charWeaponObject.ObjCount == 0) charWeaponObject.Instantiate(-190, 19, !isFlip);

                return true;
            }
            return false;
        }

        public void PushBack()
        {
            if (!isFlip) charX -= 30;
            else if (isFlip) charX += 30;
            
            charY -= 30;
        }

        public override void Update()
        {
            isAttacked = false;
            CollisionUpdate();
            charAnimation.Update();

            //Update Trigger box position
            triggerBox.X = charX - 120;
            triggerBox.Y = charY + 39;

            charWeaponObject.Update(charX, charY);

            //Update Hit 
            if (hitManager.DealDamage(charBitmap, charX, charY) != 0)
            {
                PushBack();
                charHealth -= hitManager.CurrentDamage;
            }

            if (!ScanForPlayer(player) && SplashKit.AnimationCurrentCell(charAnimation) == 14 && SplashKit.AnimationName(charAnimation) != "idle")
                charAnimation.Assign("idle");
        }

        public override void Draw()
        {
            SplashKit.DrawBitmap(charBitmap, charX, charY, charOpt);
            charWeaponObject.Draw();
        }

    }
}
