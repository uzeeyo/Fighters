using Fighters.Contestants;
using System;
using System.Collections;
using System.Collections.Generic;
using Fighters.Buffs;
using Match.Player;
using Match.UI;
using UnityEngine;

namespace Fighters.Match.Players
{
    public class PlayerStats : MonoBehaviour
    {
        const float TIME_BEFORE_MANA_REGEN = 3.5f;

        private StatusBar _healthBar;
        private StatusBar _manaBar;
        private float _currentHealth;
        private float _maxHealth = 100;
        private float _currentMana;
        private float _maxMana = 50;
        private float _manaRegenRate;
        private Coroutine _manaRegenCoroutine;
        private readonly BuffHandler _buffs = new();

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
                _currentMana = value > _maxMana ? _maxMana : value;
                ManaPercentChanged?.Invoke(_currentMana / _maxMana);
            }
        }

        public event Action<float> ManaPercentChanged;
        public event Action<float> HealthPercentChanged;

        private void OnDisable()
        {
            HealthPercentChanged -= _healthBar.OnValueChanged;
            ManaPercentChanged -= _manaBar.OnValueChanged;
        }

        public void SetStats(StatData statData)
        {
            _maxHealth = statData.Health;
            _currentHealth = statData.Health;
            _maxMana = statData.Mana;
            _currentMana = statData.Mana;
            _manaRegenRate = statData.ManaRegenRate;
        }

        public void SetStatBars(StatusBar healthBar, StatusBar manaBar)
        {
            _healthBar = healthBar;
            _manaBar = manaBar;
            
            HealthPercentChanged += _healthBar.OnValueChanged;
            ManaPercentChanged += _manaBar.OnValueChanged;
        }

        public void SetBuffDisplay(BuffDisplay buffDisplay)
        {
            buffDisplay.Init(_buffs);
        }

        public void TakeDamage(float damage)
        {
            if (_buffs.TryGetBuff(BuffType.Shield, out _))
            {
                _buffs.Remove(BuffType.Shield);
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

        public bool TryUseMana(float mana)
        {
            if (CurrentMana < mana)
            {
                return false;
            }

            CurrentMana -= mana;
            if (_manaRegenCoroutine != null)
            {
                StopCoroutine(_manaRegenCoroutine);
            }
            _manaRegenCoroutine = StartCoroutine(RegenMana());
            return true;
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

    }
}