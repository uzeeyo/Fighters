using UnityEngine;

namespace Fighters.Buffs
{
    [CreateAssetMenu(fileName = "NewBuffData", menuName = "ScriptableObjects/BuffData")]
    public class BuffData : ScriptableObject
    {
        [field: SerializeField] public float Duration;
        [field: SerializeField] public BuffType BuffType;
    }
}