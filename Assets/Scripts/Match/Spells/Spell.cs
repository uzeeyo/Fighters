using Fighters.Match.Spells;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace Fighters.Match
{
    public abstract class Spell : MonoBehaviour
    {
        protected const float MAX_TRAVEL_DISTANCE = 15f;

        private string _name;
        private string _description;
        private float _manaCost;
        private TargetType _targetType;
        private Sprite _icon;
        private float _cooldown;
        private float _castTime;
        protected VisualEffect _vfx;

        public string Name => _name;
        public string Description => _description;
        public float ManaCost => _manaCost;
        public TargetType TargetType => _targetType;
        public Sprite Icon => _icon;
        public float Cooldown => _cooldown;
        public float CastTime => _castTime;

        private void Awake()
        {
            StartCoroutine(DelayDestroy());
        }

        public virtual void Init(SpellData data)
        {
            _name = data.Name;
            _description = data.Description;
            _manaCost = data.ManaCost;
            _targetType = data.TargetType;
            _icon = data.Icon;
            _cooldown = data.Cooldown;
            _castTime = data.CastTime;
            _vfx = GetComponent<VisualEffect>();
        }

        public abstract IEnumerator Cast(Tile origin);

        private IEnumerator DelayDestroy()
        {
            yield return new WaitForSeconds(2.5f);
            Destroy(gameObject);
        }
    }
}