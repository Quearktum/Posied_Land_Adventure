using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    class EnemyPlant : Character
    {
        Player player;

        public EnemyPlant(double PlantX, double PlantY, Bitmap terrain, bool isFlip, Player player)
        {
            charX = PlantX;
            charY = PlantY;
            charHealth = 10;

            base.isFlip = isFlip;
            isOnGround = false;
            isAttacked = false;

            charBitmap = new Bitmap("Plant", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\Enemies\\EnemyPlant.png");
            charBitmap.SetCellDetails(138, 96, 2, 4, 8);

            triggerBox = new Rectangle();
            triggerBox.Width = 400;
            triggerBox.Height = 30;

            base.terrain = terrain;

            charAnimScript = new AnimationScript("plant", "plant.txt");
            charAnimation = SplashKit.CreateAnimation(charAnimScript, "idle");
            charOpt = SplashKit.OptionWithAnimation(charAnimation);

            if (!isFlip)
                charOpt = SplashKit.OptionFlipY(charOpt);

            this.player = player;

            charWeaponObject = new InstantiateObjects();
            charWeaponObject.SetObjectToInstan(objectToInstan.Pea);

            hitManager = new HitManager();
            hitManager.AddDamageableType(objectToInstan.Arrow);
        }

        public bool ScanForPlayer(Player player)
        {
            if (SplashKit.BitmapRectangleCollision(player.CharacterBmp, player.CharacterX, player.CharacterY, triggerBox) && isAttacked == false)
            {
                if (SplashKit.AnimationName(charAnimation) != "attack")
                    charAnimation.Assign("attack");

                isAttacked = true;
                if (charWeaponObject.ObjCount == 0)
                    charWeaponObject.Instantiate(30, 30, isFlip);

                return true;
            }
            return false;
        }

        public override void Update()
        {
            isAttacked = false;
            CollisionUpdate();
            charAnimation.Update();

            //Update Trigger box position
            triggerBox.X = charX - 400;
            triggerBox.Y = charY + 39;

            charWeaponObject.Update(charX, charY);

            //Update Hit 
            if (hitManager.DealDamage(charBitmap, charX, charY) != 0)
                charHealth -= hitManager.CurrentDamage;

            if (!ScanForPlayer(player) && SplashKit.AnimationName(charAnimation) != "idle") 
                charAnimation.Assign("idle");
        }

        public override void Draw()
        {
            SplashKit.DrawBitmap(charBitmap, charX, charY, charOpt);
            charWeaponObject.Draw();
        }
    }
}
