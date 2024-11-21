using UnityEngine;

namespace Fighters.Match.Spells
{
    [CreateAssetMenu(fileName = "HealData", menuName = "ScriptableObjects/SpellData/HealData")]
    public class HealData : SpellData
    {
        [field: SerializeField] public float HealAmount { get; set; }
        public override SpellType SpellType => SpellType.Heal;
    }
}