using System.Collections.Generic;
using UnityEngine;

namespace Fighters.Match.Player
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private StatusBar _healthBar;
        [SerializeField] private StatusBar _manaBar;

        private float _currentHealth;
        private float _maxHealth = 100;
        private float _currentMana;
        private float _maxMana = 50;
        private List<Buff> _buffs = new();

        public List<Buff> Buffs => _buffs;

        private void Awake()
        {
            _currentHealth = _maxHealth;
            _currentMana = _maxMana;
        }

        public void SetStatusBars(StatusBar health, StatusBar mana)
        {
            _healthBar = health;
            _manaBar = mana;
        }

        public void TakeDamage(float damage)
        {
            for (int i = 0; i < _buffs.Count; i++)
            {
                if (_buffs[i].Type == Buff.BuffType.Shielded)
                {
                    _buffs[i].Delete();
                    _buffs.RemoveAt(i);
                    return;
                }
            }

            _currentHealth -= damage;
            StartCoroutine(_healthBar.UpdateBar(_currentHealth / _maxHealth));
        }

        public void UseMana(float mana)
        {
            _currentMana -= mana;
            StartCoroutine(_manaBar.UpdateBar(_currentMana / _maxMana));
        }

        public void AddBuff(Buff newBuff)
        {
            for (int i = 0; i < _buffs.Count; i++)
            {
                if (_buffs[i].Type == newBuff.Type)
                {
                    _buffs.RemoveAt(i);
                    break;
                }
            }
            _buffs.Add(newBuff);
        }
    }
}