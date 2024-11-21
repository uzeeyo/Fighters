using UnityEngine;

namespace Fighters.Match.Spells
{
    [CreateAssetMenu(fileName = "DamageData", menuName = "ScriptableObjects/SpellData/DamageData")]
    public class DamageData : SpellData
    {
        [SerializeField] private float _damageAmount;
        public float DamageAmount => _damageAmount;
        public override SpellType SpellType => SpellType.Damage; 
    }
}