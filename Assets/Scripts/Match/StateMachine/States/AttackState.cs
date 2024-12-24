using UnityEngine;

namespace Fighters.Match
{
    public class AttackState : IState
    {
        public AttackState(StateMachine fsm)
        {
            _fsm = fsm;
            _spellCaster = _fsm.GetComponent<SpellCaster>();
        }

        private readonly StateMachine _fsm;
        private readonly SpellCaster _spellCaster;
        private float _startTime;
        private float _attackTIme;
        
        public void Enter()
        {
            _startTime = Time.time;
            _attackTIme = _spellCaster.CastRandom();
        }

        public void Exit()
        {
            //throw new System.NotImplementedException();
        }

        public void Tick()
        {
            if (Time.time - _startTime < _attackTIme) return;
            
            _fsm.ChangeState(StateType.Idle);
        }
    }
}