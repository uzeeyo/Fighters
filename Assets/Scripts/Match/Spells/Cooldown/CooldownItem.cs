namespace Fighters.Match.Spells
{
    public class CooldownItem
    {
        public CooldownItem(string name, float timeRemaining)
        {
            Name = name;
            TimeRemaining = timeRemaining;
        }

        public string Name { get; set; }
        public float TimeRemaining { get; set; }
    }
}