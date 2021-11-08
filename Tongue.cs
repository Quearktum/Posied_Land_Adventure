using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    class Tongue : InstantiateableObject
    {
        public Tongue(double tongueX, double tongueY, bool isFlip)
        {
            objX = tongueX;
            objY = tongueY;
            objDamage = 2;

            this.isFlip = isFlip;
            index = 0;
            liveTime = 40;

            objBitmap = new Bitmap("Tongue", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\Objects\\FrogTounge.png");
            objBitmap.SetCellDetails(216, 72, 2, 4, 8);

            objAnimScript = new AnimationScript("tongue", "tongue.txt");
            objAnimation = SplashKit.CreateAnimation(objAnimScript, "idle");
            objOpt = SplashKit.OptionWithAnimation(objAnimation);

            if (isFlip == true) objOpt = SplashKit.OptionFlipY(objOpt);
        }

        public override void Update()
        {
            objAnimation.Update();

            if (index < liveTime) index += 1;
            else IsDestroyed = true;
        }

        public override void Draw()
        {
            if (index < liveTime) SplashKit.DrawBitmap(objBitmap, objX, objY, objOpt);
        }

    }
}
