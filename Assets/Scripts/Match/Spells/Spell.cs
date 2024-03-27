using Fighters.Match.Spells;
using Fighters.Utils;
using UnityEngine;
using UnityEngine.VFX;

namespace Fighters.Match
{
    public abstract class Spell : MonoBehaviour
    {
        private string _name;
        private string _description;
        private float _manaCost;
        private TargetType _targetType;
        private Sprite _icon;
        private float _cooldown;
        private float _castTime;
        private int _range;
        private VisualEffectAsset _vfx;

        public string Name => _name;
        public string Description => _description;
        public float ManaCost => _manaCost;
        public TargetType TargetType => _targetType;
        public Sprite Icon => _icon;
        public float Cooldown => _cooldown;
        public float CastTime => _castTime;
        public VisualEffectAsset Vfx => _vfx;


        public virtual void Init(SpellData data)
        {
            _name = data.Name;
            _description = data.Description;
            _manaCost = data.ManaCost;
            _targetType = data.TargetType;
            _icon = data.Icon;
            _cooldown = data.Cooldown;
            _castTime = data.CastTime;
            _vfx = data.Vfx;
            _range = data.Range;

            GetComponent<VisualEffect>().visualEffectAsset = _vfx;
        }

        public virtual void Cast(Tile origin)
        {
            StartCoroutine(Timer.CallAfterSeconds(CastTime, () =>
            {

            }));

        }

        protected void CastForward()
        {
            var rb = GetComponent<Rigidbody>();
            StartCoroutine(Timer.CallAfterSeconds(CastTime, () =>
            {
                rb.velocity = transform.forward * 20;
            }));
        }

        protected void CastOnSingleTile(Tile origin)
        {
            var targetTile = origin.Grid.GetTile(origin.Location, new Vector2(_range, 0));
            transform.position = targetTile.transform.position;
        }
    }
}