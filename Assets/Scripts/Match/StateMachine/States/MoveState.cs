using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Fighters.Match
{
    public class MoveState : IState
    {
        private Queue<Vector2> _moveDirections;

        public MoveState(StateMachine fsm)
        {
            _fsm = fsm;

        }

        private StateMachine _fsm;

        public void Enter()
        {
            var tempList = new List<Vector2>()
            {
                Vector2.up,
                Vector2.down,
                Vector2.left,
                Vector2.right
            };
            _moveDirections = new Queue<Vector2>(tempList.OrderBy(x => Guid.NewGuid()));

            Move();
        }

        private void Move()
        {
            if (_moveDirections.Count == 0)
            {
                _fsm.ChangeState(StateType.Idle);
                return;
            }

            if (!_fsm.Self.MovementController.TryMove(_moveDirections.Dequeue()))
            {
                Move();
            }
        }

        public void Exit()
        {

        }

        public void Tick()
        {
        }
    }
}