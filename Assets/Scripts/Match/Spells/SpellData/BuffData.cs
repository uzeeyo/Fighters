using Fighters.Match.Spells;
using UnityEngine;
using UnityEngine.VFX;

namespace Fighters.Buffs
{
    public enum BuffType
    {
        Poison,
        Burn,
        Stun,
        Silence,
        Root,
        Slow,
        Confusion,
        Blind,
        Shield,
    }
    
    [CreateAssetMenu(fileName = "BuffData", menuName = "Spell/SpellData/BuffData", order = 0)]
    public class BuffData : SpellData
    {
        [SerializeField] private BuffType _buffType;
        [SerializeField] private float _buffDuration;
        [SerializeField] private float _hPPS;
        [SerializeField] private VisualEffectAsset _buffVFX;
        
        public override SpellType SpellType => SpellType.Buff;
        public BuffType BuffType => _buffType;
        public float BuffDuration => _buffDuration;
        public float HPPS => _hPPS;
        public VisualEffectAsset BuffVFX => _buffVFX;
    }
}