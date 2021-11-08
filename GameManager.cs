using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    public class GameManager
    {
        private List<Character> characterList;
        private List<IPickupAble> pickupList;

        public GameManager(IEnumerable<Character> characters, IEnumerable<IPickupAble> pickups)
        {
            characterList = new List<Character>(characters);
            pickupList = new List<IPickupAble>(pickups);
        }

        public void Update()
        {
            for (int i = 0; i < characterList.Count; i++)
            {
                if (characterList[i] != null)
                {
                    characterList[i].Update();

                    if (characterList[i].Health <= 0)
                    {
                        for (int n = 0; n < characterList[i].CharacterWeapon.Objects.Count; n++)
                        {
                            characterList[i].CharacterWeapon.Objects[n] = null;
                            characterList[i].CharacterWeapon.Objects.RemoveAt(n);
                        }                            
                        characterList[i] = null;
                        characterList.RemoveAt(i);
                    }
                }
            }

            for(int i = 0; i < pickupList.Count; i++)
            {
                if (pickupList[i] != null)
                {
                    pickupList[i].Update();

                    if (pickupList[i].IsPicked)
                    {
                        pickupList[i].Update();

                        pickupList[i] = null;
                        pickupList.RemoveAt(i);
                    }
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < characterList.Count; i++)
            {
                if(characterList[i] != null)
                    characterList[i].Draw();
            }

            for (int i = 0; i < pickupList.Count; i++)
            {
                if (pickupList[i] != null)
                    pickupList[i].Draw();
            }
        }
    }
}
