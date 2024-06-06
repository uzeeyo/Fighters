using Fighters.Match.Players;
using System.Collections.Generic;
using UnityEngine;

namespace Fighters.Match
{
    public class StateMachine : MonoBehaviour
    {
        protected IState _currentState;
        protected Dictionary<StateType, IState> _states;

        public Animator Animator { get; private set; }
        public Player Self { get; private set; }
        public Player Opponent { get; private set; }

        public virtual void Awake()
        {
            Self = GetComponent<Player>();
            _states = new()
            {
                { StateType.Idle, new IdleState(this) },
                { StateType.Move, new MoveState(this) },
            };
            Animator = GetComponent<Animator>();

        }

        public void ChangeState(StateType stateType)
        {
            _currentState?.Exit();
            _currentState = _states[stateType];
            _currentState.Enter();

        }

        private void Update()
        {
            _currentState.Tick();
        }
    }
}