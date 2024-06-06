using UnityEngine;

namespace Fighters.Match
{
    public class IdleState : IState
    {
        public IdleState(StateMachine fsm)
        {
            _fsm = fsm;
        }

        private StateMachine _fsm;
        private float _timeSinceEnter;
        private float _timeToAction;

        public void Enter()
        {
            _timeSinceEnter = 0f;
            _timeToAction = Random.Range(1f, 3f);
        }

        public void Exit()
        {

        }

        public void Tick()
        {
            _timeSinceEnter += Time.deltaTime;

            if (_timeSinceEnter > _timeToAction)
            {
                _fsm.ChangeState(StateType.Move);
                return;
            }
        }
    }
}