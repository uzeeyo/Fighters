using UnityEngine;

namespace Fighters.Buffs
{
    public enum BuffType
    {
        Poison,
        Stun,
        Silence,
        Root,
        Slow,
        Confuse,
        Blind,
        Shield,
    }
    
    [CreateAssetMenu(fileName = "NewBuffData", menuName = "ScriptableObjects/BuffData")]
    public class BuffData : ScriptableObject
    {
        [field: SerializeField] public float Duration;
        [field: SerializeField] public BuffType BuffType;
        [field: SerializeField] public Sprite Icon;
    }
}