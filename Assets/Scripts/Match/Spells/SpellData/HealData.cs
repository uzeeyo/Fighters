using UnityEngine;

namespace Fighters.Match.Spells
{
    [CreateAssetMenu(fileName = "HealData", menuName = "Spell/SpellData/HealData", order = 0)]
    public class HealData : SpellData
    {
        [SerializeField] private float _healAmount;
        public float HealAmount => _healAmount;
        public override SpellType SpellType => SpellType.Heal;
    }
}