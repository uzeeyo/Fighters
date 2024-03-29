using UnityEngine;

namespace Fighters.Contestants
{
    [CreateAssetMenu(fileName = "StatData", menuName = "ScriptableObjects/StatData", order = 0)]
    public class StatData : ScriptableObject
    {
        [SerializeField] private float _health;
        [SerializeField] private float _mana;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _manaRegenRate;

        public float Health => _health;
        public float Mana => _mana;
        public float MoveSpeed => _moveSpeed;
        public float ManaRegenRate => _manaRegenRate;
    }
}