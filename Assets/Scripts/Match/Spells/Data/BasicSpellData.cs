using UnityEngine;

namespace Fighters.Match.Spells
{
    public class BasicSpellData : ISpellData, IDamageData, ITravelData
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _castTime;
        [SerializeField] private TargetType _targetType;
        [SerializeField] private float _damage;
        [SerializeField] private float _travelSpeed;
        [SerializeField] private int _range;

        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public float Cooldown => _cooldown;
        public float CastTime => _castTime;
        public TargetType TargetType => _targetType;
        public float Damage => _damage;
        public float TravelSpeed => _travelSpeed;
        public int Range => _range;
    }
}