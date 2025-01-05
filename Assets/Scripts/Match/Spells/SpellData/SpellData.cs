using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

namespace Fighters.Match.Spells
{
    public enum TargetType
    {
        Self,
        Single,
        SingleRandom,
        MultiMoveDelayed,
        MoveForward,
        SingleMoveToTile,
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
        Center
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
        [SerializeField] private bool _hasCastingVFX;
        [SerializeField] private VisualEffectAsset _castingVFXEffect;
        [SerializeField] private SpawnLocation _spawnLocation;
        [SerializeField] private bool _shakesOnImpact;
        [SerializeField] private bool _shakesOnCast;
        [SerializeField] private float _shakeDuration;
        [SerializeField] private float _shakeStrength;
        [SerializeField] private bool _changesTileState;
        [SerializeField] private float _targetDelayAfterCast;

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
        public bool HasCastingVFX => _hasCastingVFX;
        public bool ShakesOnImpact => _shakesOnImpact;
        public bool ShakesOnCast => _shakesOnCast;
        public float ShakeDuration => _shakeDuration;
        public float ShakeStrength => _shakeStrength;
        public float TargetDelayAfterCast => _targetDelayAfterCast;
        
        //Tile state modifiers
        [SerializeField] private TileState _tileState;
        [SerializeField] private float _tileStateDuration;
        
        public TileState TileState => _tileState;
        public float TileStateDuration => _tileStateDuration;



        //Targeting
        [SerializeField] private TargetType _targetType;
        [SerializeField] private Side _targetSide;
        [SerializeField] private int _range;
        [SerializeField] private float _randomTimeInterval;
        [SerializeField] private AnimationCurve _horizontalCurve;
        [SerializeField] private AnimationCurve _verticalCurve;
        [SerializeField] private AnimationCurve _speedCurve;
        [SerializeField] private bool _hasDuration;
        [FormerlySerializedAs("_duration")] [SerializeField] private float _targetDuration;

        public TargetType TargetType => _targetType;
        public Side TargetSide => _targetSide;
        public int Range => _range;
        public float RandomTimeInterval => _randomTimeInterval;
        public AnimationCurve HorizontalCurve => _horizontalCurve;
        public AnimationCurve VerticalCurve => _verticalCurve;
        public AnimationCurve SpeedCurve => _speedCurve;
        public abstract SpellType SpellType { get; }
        public bool HasDuration => _hasDuration;
        public float TargetDuration => _targetDuration;
    }
}