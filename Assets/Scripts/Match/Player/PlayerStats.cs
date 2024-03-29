using Fighters.Contestants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fighters.Match.Players
{
    public class PlayerStats : MonoBehaviour
    {
        const float TIME_BEFORE_MANA_REGEN = 3.5f;

        [SerializeField] private StatusBar _healthBar;
        [SerializeField] private StatusBar _manaBar;

        private float _currentHealth;
        private float _maxHealth = 100;
        private float _currentMana;
        private float _maxMana = 50;
        private float _manaRegenRate;
        private Coroutine _manaRegenCoroutine;
        private List<Buff> _buffs = new();

        public float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                if (value > _maxHealth)
                {
                    _currentHealth = _maxHealth;
                }
                else
                {
                    _currentHealth = value;
                }
                _currentHealth = value;
                HealthPercentChanged?.Invoke(_currentHealth / _maxHealth);
            }
        }

        public float CurrentMana
        {
            get => _currentMana;
            set
            {
                if (value > _maxMana)
                {
                    _currentMana = _maxMana;
                }
                else
                {
                    _currentMana = value;
                }
                ManaPercentChanged?.Invoke(_currentMana / _maxMana);
            }
        }

        public event Action<float> ManaPercentChanged;
        public event Action<float> HealthPercentChanged;

        public List<Buff> Buffs => _buffs;

        private void Awake()
        {
            HealthPercentChanged += _healthBar.OnValueChanged;
            ManaPercentChanged += _manaBar.OnValueChanged;
        }

        private void OnDisable()
        {
            HealthPercentChanged -= _healthBar.OnValueChanged;
            ManaPercentChanged -= _manaBar.OnValueChanged;
        }

        public void Init(StatData statData)
        {
            _maxHealth = statData.Health;
            _currentHealth = statData.Health;
            _maxMana = statData.Mana;
            _currentMana = statData.Mana;
            _manaRegenRate = statData.ManaRegenRate;
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

            CurrentHealth -= damage;
        }

        public void Heal(float amount)
        {

            if (_currentHealth + amount > _maxHealth)
            {
                CurrentHealth = _maxHealth;
            }
            else
            {
                CurrentHealth += amount;
            }
        }

        public void UseMana(float mana)
        {
            CurrentMana -= mana;
            if (_manaRegenCoroutine != null)
            {
                StopCoroutine(_manaRegenCoroutine);
            }
            _manaRegenCoroutine = StartCoroutine(RegenMana());
        }

        private IEnumerator RegenMana()
        {
            yield return new WaitForSeconds(TIME_BEFORE_MANA_REGEN);

            float manaToRegen = _maxMana - CurrentMana;
            float timeToRegen = manaToRegen / _manaRegenRate;
            float timeElapsed = 0;
            while (timeElapsed < timeToRegen)
            {
                CurrentMana = CurrentMana + (_manaRegenRate * Time.deltaTime);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            CurrentMana = _maxMana;
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