using UnityEngine;

namespace Fighters.Match
{
    public class AttackState : IState
    {
        public void Enter()
        {
            Debug.Log("Attacking");
        }

        public void Exit()
        {
            //throw new System.NotImplementedException();
        }

        public void Tick()
        {
            //throw new System.NotImplementedException();
        }
    }
}