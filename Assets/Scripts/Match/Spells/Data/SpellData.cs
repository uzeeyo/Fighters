using UnityEngine;
using UnityEngine.VFX;

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
        [SerializeField] private VisualEffectAsset _vfx;
        [SerializeField] private GameObject _spellPrefab;
        [SerializeField] private float _damage;
        [SerializeField] private float _travelSpeed;
        [SerializeField] private int _range;

        public string Name => _name;
        public string Description => _description;
        public float ManaCost => _manaCost;
        public Sprite Icon => _icon;
        public float Cooldown => _cooldown;
        public float CastTime => _castTime;
        public SpellType SpellType => _spellType;
        public TargetType TargetType => _targetType;
        public VisualEffectAsset Vfx => _vfx;
        public GameObject SpellPrefab => _spellPrefab;
        public float Damage => _damage;
        public float TravelSpeed => _travelSpeed;
        public int Range => _range;
    }
}