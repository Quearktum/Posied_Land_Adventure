using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    public abstract class InstantiateableObject
    {
        protected Bitmap objBitmap;

        protected double objX, objY;
        protected int index;
        protected int liveTime;
        protected int objDamage;

        protected bool isFlip;
        protected bool isDestroy = false;

        protected AnimationScript objAnimScript;
        protected Animation objAnimation;
        protected DrawingOptions objOpt;

        public double ObjectX { get { return objX; } }
        public double ObjectY { get { return objY; } }
        public int Damage { get { return objDamage; } }
        public Bitmap ObjectBmp { get { return objBitmap; } }
        public DrawingOptions ObjectOpt { get { return objOpt; } }
        public bool IsDestroyed { get { return isDestroy; } set { isDestroy = value; } }
        
        public abstract void Update();

        public abstract void Draw();
    }
}
