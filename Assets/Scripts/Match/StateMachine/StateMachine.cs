using Fighters.Match.Players;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Fighters.Match
{
    public class StateMachine : MonoBehaviour
    {
        protected IState _currentState;
        protected Dictionary<StateType, IState> _states;

        public Player Self { get; private set; }
        public Player Opponent { get; private set; }

        public virtual void Awake()
        {
            Self = GetComponent<Player>();
            Opponent = FindObjectsByType<Player>(FindObjectsSortMode.None).FirstOrDefault(x => x.Side != Self.Side);
            _states = new()
            {
                { StateType.Idle, new IdleState(this) },
                { StateType.Move, new MoveState(this) },
                { StateType.Attack, new AttackState(this) }
            };
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