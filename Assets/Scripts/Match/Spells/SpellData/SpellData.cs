using UnityEngine;
using UnityEngine.Serialization;

namespace Fighters.Match.Spells
{
    public enum TargetType
    {
        Self,
        Single,
        SingleRandom,
        MultiRandomDelayed,
        MoveForward,
        MultiForward,
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
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private string _animationName;
        [SerializeField] private float _manaCost;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _castTime;
        [SerializeField] private Spell _prefab;
        [SerializeField] private float _travelTime;
        [SerializeField] private SpawnLocation _spawnLocation;

        public string Name => _name;
        public string Description => _description;
        public string AnimationName => _animationName;
        public float ManaCost => _manaCost;
        public Sprite Icon => _icon;
        public float Cooldown => _cooldown;
        public float CastTime => _castTime;
        public Spell Prefab => _prefab;
        public float TravelTime => _travelTime;
        public SpawnLocation SpawnLocation => _spawnLocation;



        //Targeting
        [SerializeField] private TargetType _targetType;
        [SerializeField] private Side _targetSide;
        [SerializeField] private int _range;
        [SerializeField] private int _randomTimeInterval;
        [SerializeField] private float _instantDelay;
        [SerializeField] private AnimationCurve _horizontalCurve;
        [SerializeField] private AnimationCurve _verticalCurve;
        [SerializeField] private bool _hasDuration;
        [SerializeField] private float _duration;

        public TargetType TargetType => _targetType;
        public Side TargetSide => _targetSide;
        public int Range => _range;
        public float RandomTimeInterval => _randomTimeInterval;
        public float InstantDelay => _instantDelay;
        public AnimationCurve HorizontalCurve => _horizontalCurve;
        public AnimationCurve VerticalCurve => _verticalCurve;
        public abstract SpellType SpellType { get; }
        public bool HasDuration => _hasDuration;
        public float Duration => _duration;
    }
}