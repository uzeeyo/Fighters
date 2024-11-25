using UnityEngine;
using UnityEngine.VFX;

namespace Fighters.Match.Spells
{
    [CreateAssetMenu(fileName = "DamageData", menuName = "ScriptableObjects/SpellData/DamageData")]
    public class DamageData : SpellData
    {
        [SerializeField] private float _damageAmount;
        [SerializeField] private VisualEffectAsset _hitEffect;
        
        public float DamageAmount => _damageAmount;
        public VisualEffectAsset HitEffect => _hitEffect;
        public override SpellType SpellType => SpellType.Damage; 
    }
}