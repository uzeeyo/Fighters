using UnityEngine;

namespace Fighters.Match.Spells
{
    [CreateAssetMenu(fileName = "SpellData", menuName = "ScriptableObjects/SpellData")]
    public class SpellData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private float _manaCost;
        [SerializeField] private Sprite _icon;
        [SerializeField] private SpellType _spellType;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _castTime;
        [SerializeField] private TargetType _targetType;
        [SerializeField] private Spell _prefab;
        [SerializeField] private float _damage;
        [SerializeField] private float _travelTime;
        [SerializeField] private int _range;
        [SerializeField] private AnimationCurve _horizantalCurve;
        [SerializeField] private AnimationCurve _verticalCurve;

        public string Name => _name;
        public string Description => _description;
        public float ManaCost => _manaCost;
        public Sprite Icon => _icon;
        public float Cooldown => _cooldown;
        public float CastTime => _castTime;
        public SpellType SpellType => _spellType;
        public TargetType TargetType => _targetType;
        public Spell Prefab => _prefab;
        public float Damage => _damage;
        public float TravelTime => _travelTime;
        public int Range => _range;
        public AnimationCurve HorizantalCurve => _horizantalCurve;
        public AnimationCurve VerticalCurve => _verticalCurve;
    }
}