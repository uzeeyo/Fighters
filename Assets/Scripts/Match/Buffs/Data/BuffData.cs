using UnityEngine;

namespace Fighters.Buffs
{
    public enum BuffType
    {
        Poisoned,
        Stunned,
        Silenced,
        Rooted,
        Slowed,
        Confused,
        Blinded,
        Shielded,
    }
    
    [CreateAssetMenu(fileName = "NewBuffData", menuName = "ScriptableObjects/SpellData/BuffData")]
    public class BuffData : ScriptableObject
    {
        [field: SerializeField] public float Duration;
        [field: SerializeField] public BuffType BuffType;
    }
}