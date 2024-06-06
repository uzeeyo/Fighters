using UnityEngine;

namespace Fighters.Match
{
    public class ThinkState : IState
    {
        public ThinkState(AiStateMachine fsm)
        {
            r_fsm = fsm;
        }

        private const float MINIMUM_THINK_TIME = 0.2f;
        private readonly AiStateMachine r_fsm;

        private float _chanceToAttack;
        private float _timeSinceLastAction;

        public void Enter()
        {
            _timeSinceLastAction = 0;

            //TODO: If valid target, high chance to attack
            _chanceToAttack = 0;
        }

        public void Exit()
        {

        }

        public void Tick()
        {
            _timeSinceLastAction += Time.deltaTime;


            if (_timeSinceLastAction >= MINIMUM_THINK_TIME)
            {
                float random = Random.Range(0f, 1f);
                if (random < _chanceToAttack)
                {
                    r_fsm.ChangeState(StateType.Attack);
                    return;
                }

                if (random < 0.5)
                {
                    r_fsm.ChangeState(StateType.Move);
                    return;
                }
            }
        }
    }
}