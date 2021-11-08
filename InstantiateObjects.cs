using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    public class InstantiateObjects
    {
        private double pX, pY;
        private List<InstantiateableObject> objects;
        private objectToInstan selectedObject;

        public int ObjCount { get { return objects.Count; } }
        public List<InstantiateableObject> Objects { get { return objects; } }
        public objectToInstan SelectedObject { get { return selectedObject; } }

        public InstantiateObjects()
        {
            objects = new List<InstantiateableObject>();
        }

        public void SetObjectToInstan(objectToInstan obj)
        {
            selectedObject = obj;
        }

        public void Instantiate(double offsetX, double offsetY, bool isFlip)
        {
            if(selectedObject == objectToInstan.Arrow)
            {
                Arrow arrow = new Arrow(pX + offsetX, pY + offsetY, isFlip);
                objects.Add(arrow);
            }

            if(selectedObject == objectToInstan.Tongue)
            {
                Tongue tongue = new Tongue(pX + offsetX, pY + offsetY, isFlip);
                objects.Add(tongue);
            }

            if(selectedObject == objectToInstan.Pea)
            {
                Pea pea = new Pea(pX + offsetX, pY + offsetY, isFlip);
                objects.Add(pea);
            }
        }

        public void Update(double posX, double posY)
        {
            pX = posX;
            pY = posY;

            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] != null)
                {
                    objects[i].Update();

                    if (objects[i].IsDestroyed)
                    {
                        objects[i] = null;
                        objects.RemoveAt(i);
                    }
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] != null)
                    objects[i].Draw();
            }
        }

    }
}
