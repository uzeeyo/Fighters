using UnityEngine;

namespace Fighters.Match.Spells
{
    [CreateAssetMenu(fileName = "DamageData", menuName = "ScriptableObjects/SpellData/DamageData")]
    public class DamageData : SpellData
    {
        [field: SerializeField] public int DamageAmount { get; private set; }
        public override SpellType SpellType => SpellType.Damage; 
    }
}