using UnityEngine;

namespace Fighters.Match
{
    public class IdleState : IState
    {
        public IdleState(StateMachine fsm)
        {
            _fsm = fsm;
        }

        private readonly StateMachine _fsm;
        private float _timeSinceEnter;
        private float _timeToAction;
        private float _chanceToAttack;

        public void Enter()
        {
            _timeSinceEnter = 0f;
            _timeToAction = Random.Range(0.5f, 2f);
            _chanceToAttack = Random.Range(0f, 0.4f);
        }

        public void Exit()
        {

        }

        public void Tick()
        {
            _timeSinceEnter += Time.deltaTime;

            if (_timeSinceEnter > _timeToAction)
            {
                DecideNextState();
            }
        }

        private void DecideNextState()
        {
            if (Random.Range(0f, 1f) < _chanceToAttack)
            {
                _fsm.ChangeState(StateType.Attack);
                return;
            }
            _fsm.ChangeState(StateType.Move);
        }
    }
}