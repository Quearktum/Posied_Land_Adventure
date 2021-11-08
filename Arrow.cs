using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    class Arrow : InstantiateableObject
    {
        public Arrow(double arrowX, double arrowY, bool isFlip)
        {
            objX = arrowX;
            objY = arrowY;
            this.isFlip = isFlip;
            index = 0;
            liveTime = 60;
            objDamage = 5;

            objBitmap = new Bitmap("Arrow", "E:\\Uni\\COS20007\\Custom_Program\\Resources\\Images\\Objects\\Arrow.png");
            objBitmap.SetCellDetails(60, 60, 2, 2, 4);          

            objAnimScript = new AnimationScript("arrow", "arrow.txt");
            objAnimation = SplashKit.CreateAnimation(objAnimScript, "idle");
            objOpt = SplashKit.OptionWithAnimation(objAnimation);

            if (this.isFlip == true) objOpt = SplashKit.OptionFlipY(objOpt);
        }

        public override void Update()
        {
            objAnimation.Update();
          
            if (index < liveTime)
            {
                if(index > 14)
                Shoot();

                index += 1;
            }
            else IsDestroyed = true;        
        }

        public override void Draw()
        { 
            if(index < liveTime) SplashKit.DrawBitmap(objBitmap, objX, objY, objOpt);
        }

        public void Shoot()
        {
            if (isFlip == false) objX += 20;
            else if (isFlip == true) objX -= 20;
        }

    }
}
