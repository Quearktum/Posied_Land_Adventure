using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    class Pea : InstantiateableObject
    {
        public Pea(double PeaX, double PeaY, bool isFlip)
        {
            objX = PeaX;
            objY = PeaY;
            this.isFlip = isFlip;
            index = 0;
            liveTime = 120;
            objDamage = 1;

            objBitmap = new Bitmap("Pea", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\Objects\\EnemyPlantPea.png");
            objBitmap.SetCellDetails(34, 34, 2, 2, 4);

            objAnimScript = new AnimationScript("pea", "pea.txt");
            objAnimation = SplashKit.CreateAnimation(objAnimScript, "idle");
            objOpt = SplashKit.OptionWithAnimation(objAnimation);

            if (this.isFlip == true)
                objOpt = SplashKit.OptionFlipY(objOpt);
        }

        public override void Update()
        {
            objAnimation.Update();

            if (index < liveTime)
            {
                if (index > 14)
                    Shoot();

                index += 1;
            }
            else
                IsDestroyed = true;
        }

        public override void Draw()
        {
            if (index < liveTime)
                SplashKit.DrawBitmap(objBitmap, objX, objY, objOpt);
        }

        public void Shoot()
        {
            if (isFlip == false)
                objX += 10;
            else if (isFlip == true)
                objX -= 10;
        }
    }
}
