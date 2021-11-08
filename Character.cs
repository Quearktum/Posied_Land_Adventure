using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    public abstract class Character
    {
        protected double charX, charY;
        protected int charHealth;

        protected bool isFlip;
        protected bool isOnGround;
        protected bool isAttacked;

        protected Rectangle triggerBox;
        protected Bitmap charBitmap;
        protected Bitmap terrain;

        protected AnimationScript charAnimScript;
        protected Animation charAnimation;
        protected DrawingOptions charOpt;

        protected InstantiateObjects charWeaponObject;
        protected HitManager hitManager;

        public double CharacterX { get { return charX; } }
        public double CharacterY { get { return charY; } }
        public int Health { get { return charHealth; } set { charHealth = value; } }
        public Bitmap CharacterBmp { get { return charBitmap; } }
        public DrawingOptions CharacterOpt { get { return charOpt; } }
        public InstantiateObjects CharacterWeapon { get { return charWeaponObject; } }
        public HitManager HitManager { get { return hitManager; } }

        public virtual void CollisionUpdate()
        {
            if (SplashKit.BitmapCollision(charBitmap, charX, charY, terrain, 0, 0))
            {
                if (isOnGround != true)
                    isOnGround = true;
            }
            else
            {
                if (isOnGround != false)
                    isOnGround = false;

                charY += 5;
            }
        }

        public void Fall()
        {
            if (charY > 800)
                charHealth = 0;
        }

        public abstract void Update();

        public abstract void Draw();
    }
}
