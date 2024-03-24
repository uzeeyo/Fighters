using Fighters.Match.Spells;

namespace Fighters.Match
{
    public interface ISpell
    {
        public ISpellData Data { get; }

        public void Cast();
    }
}