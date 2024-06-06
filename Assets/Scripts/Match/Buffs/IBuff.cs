namespace Fighters.Buffs
{
    public interface IBuff
    {
        public BuffType BuffType { get; }
        public float Duration { get; }
        public void Activate();
        public void Deactivate();
    }
}