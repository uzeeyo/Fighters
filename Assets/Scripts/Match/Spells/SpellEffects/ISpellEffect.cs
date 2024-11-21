using Fighters.Match.Players;
using UnityEngine;

namespace Fighters.Match.Spells
{
    public interface ISpellEffect
    {
        void Apply(PlayerStats stats);
    }
}