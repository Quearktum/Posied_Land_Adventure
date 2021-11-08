using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_Program
{
    public interface IPickupAble
    {
        public bool IsPicked { get; set; }
        public bool IsPickup();
        public void Update();
        public void Draw();

    }
}
