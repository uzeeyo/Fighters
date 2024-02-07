namespace Fighters.Match.Player
{
    public class Buff
    {
        public enum BuffType
        {
            Poisoned,
            Stunned,
            Silenced,
            Rooted,
            Slowed,
            Confused,
            Blinded,
            Shielded,
        }

        private BuffType _type;
        private float _duration;

        public BuffType Type => _type;

        public void Delete()
        {
            //Handle animations, sounds, etc.
        }
    }
}