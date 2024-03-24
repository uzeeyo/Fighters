namespace Fighters.Match.Spells
{
    public interface ITravelData : ISpellData
    {
        public int Range { get; }
        public float TravelSpeed { get; }
    }
}