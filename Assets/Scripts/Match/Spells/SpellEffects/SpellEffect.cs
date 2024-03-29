using Fighters.Match.Players;
using UnityEngine;

namespace Fighters.Match.Spells
{
    public abstract class SpellEffect : MonoBehaviour
    {
        public abstract void Init(SpellData data);

        public abstract void Apply(PlayerStats stats);
    }
}