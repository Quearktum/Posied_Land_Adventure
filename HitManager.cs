using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    public class HitManager
    {
        private List<objectToInstan> damageableType;
        private List<InstantiateObjects> damageableObjects;
        private int currentDamage;

        public int CurrentDamage { get { return currentDamage; } }

        public HitManager()
        {
            damageableType = new List<objectToInstan>();
            damageableObjects = new List<InstantiateObjects>();
        }

        public void AddDamageableType(objectToInstan type)
        {
            damageableType.Add(type);
        }

        public void AddDamageableObj(InstantiateObjects objs)
        {
            damageableObjects.Add(objs);
        }

        public bool IsHit(Bitmap characterBmp, double characterX, double characterY)
        {
            foreach (objectToInstan type in damageableType)
            {
                foreach (InstantiateObjects objs in damageableObjects)
                {
                    if (objs.SelectedObject == type)
                    {
                        for (int i = 0; i < objs.Objects.Count; i++)
                        {
                            if (objs.Objects[i] != null)
                            {
                                currentDamage = objs.Objects[i].Damage;

                                if (SplashKit.BitmapCollision(objs.Objects[i].ObjectBmp, objs.Objects[i].ObjectX, objs.Objects[i].ObjectY, characterBmp, characterX, characterY))
                                {
                                    objs.Objects[i].IsDestroyed = true;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public int DealDamage(Bitmap characterBmp, double characterX, double characterY)
        {
            if (IsHit(characterBmp, characterX, characterY))
                return currentDamage;
            else
                return currentDamage = 0;
        }
    }
}
