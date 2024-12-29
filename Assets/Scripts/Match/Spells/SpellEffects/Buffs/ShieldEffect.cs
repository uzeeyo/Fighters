using Fighters.Match;
using Fighters.Match.Spells;
using UnityEngine;
using UnityEngine.VFX;

namespace Fighters.Buffs
{
    public class ShieldEffect : BuffEffect
    {
        public ShieldEffect(BuffData buffData) : base(buffData)
        {
            
        }

        private SpellCaster _spellCaster;
        private VisualEffect _vfx;
        private GameObject _shieldObj;

        public override void Activate()
        {
            
            _shieldObj = new GameObject("Shield", typeof(VisualEffect));
            Object.Instantiate(_shieldObj);
            _spellCaster = _playerStats.GetComponent<SpellCaster>();
            _spellCaster.PlaceInSpellSlot(_shieldObj.transform, SpawnLocation.Center);
            _vfx = _shieldObj.GetComponent<VisualEffect>();
            _vfx.visualEffectAsset = _vfxAsset;
        }

        public override async void Deactivate()
        {
            _vfx.SetBool("Broken", true);
            await Awaitable.WaitForSecondsAsync(2);
            Object.Destroy(_shieldObj);
        }
    }
}