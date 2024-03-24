using UnityEngine;

namespace Fighters.Match.Spells
{
    public interface ISpellData
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Icon { get; }
        public float Cooldown { get; }
        public float CastTime { get; }
        public TargetType TargetType { get; }

    }
}