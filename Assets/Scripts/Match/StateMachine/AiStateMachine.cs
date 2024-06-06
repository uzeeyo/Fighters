namespace Fighters.Match
{
    public class AiStateMachine : StateMachine
    {
        public override void Awake()
        {
            base.Awake();
            _states.Add(StateType.Think, new ThinkState(this));
            ChangeState(StateType.Idle);
        }
    }
}