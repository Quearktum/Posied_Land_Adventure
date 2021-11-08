using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom_Program
{
    public class StateManager
    {
        private Dictionary<Type, GameState> states;
        private GameState currentState;

        public int CurrentStage { get; set; }

        public StateManager()
        {
            states = new Dictionary<Type, GameState>();
            CurrentStage = 1;
        }

        public void AddState<T>() where T : GameState
        {
            T state = Activator.CreateInstance<T>();
            AddState(state);
        }

        public void AddState(GameState state)
        {
            state.manager = this;
            states.Add(state.GetType(), state);
        }

        public void SetCurrentState<T>() where T : GameState
        {
            Type stateType = typeof(T);
            SetCurrentState(stateType);
        }

        public void SetCurrentState(Type newStateType)
        {
            if (states.ContainsKey(newStateType))
            {
                GameState newState = states[newStateType];

                if (currentState != null && currentState != newState)
                    currentState.OnStateExit();

                currentState = newState;
                currentState.OnStateEnter();
            }
        }

        public void CheckInput()
        {
            currentState.CheckInput();
        }
        public void Update() 
        { 
            currentState.Update();
        }

        public void Draw()
        {
            currentState.Draw();
        }
    }
}
