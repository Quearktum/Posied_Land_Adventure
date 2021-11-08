using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom_Program
{
    public abstract class GameState
    {
        public StateManager manager;

        public virtual void OnStateEnter() { }
        public virtual void OnStateExit() { }

        public abstract void CheckInput();
        public abstract void Update();
        public abstract void Draw();

    }
}
