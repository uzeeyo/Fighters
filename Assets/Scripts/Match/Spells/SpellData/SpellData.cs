using UnityEngine;

namespace Fighters.Match.Spells
{
    public enum TargetType
    {
        Self,
        Single,
        SingleRandom,
        MultiRandom,
        MoveForward,
    }
    
    public enum SpellType
    {
        Damage,
        Heal,
        Buff
    }

    public enum SpawnLocation
    {
        Default,
        LeftHand,
        RightHand,
        Weapon,
    }
    
    public abstract class SpellData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public string AnimationName { get; private set; }
        [field: SerializeField] public float ManaCost { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float CastTime { get; private set; }
        [field: SerializeField] public Spell Prefab { get; private set; }
        [field: SerializeField] public float TravelTime { get; private set; }
        [field: SerializeField] public SpawnLocation SpawnLocation { get; private set; }

        public abstract SpellType SpellType { get; }

        //Targeting
        public TargetType TargetType { get; set; }
        public int Range { get; set; }
        public float RandomTimeInterval { get; set; }
        public float InstantDelay { get; set; }
        public AnimationCurve HorizontalCurve { get; private set; }
        public AnimationCurve VerticalCurve { get; private set; }

    }
}