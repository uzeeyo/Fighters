using Fighters.Match.Players;
using Fighters.Match.Spells;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fighters.Match
{
    [RequireComponent(typeof(SpellBank), typeof(PlayerInput))]
    public class SpellCaster : MonoBehaviour
    {
        private bool _onCooldown = false;
        private SpellBank _spellBank;
        private Player _player;
        private Dictionary<SpawnLocation, Transform> _spawnLocations;

        [Header("Spell spawn positions")]
        [SerializeField] private Transform _default;
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _weapon;

        public CooldownHandler CooldownHandler { get; } = new();
        public bool IsCasting { get; private set; }
        
        private void Awake()
        {
            _spellBank = GetComponent<SpellBank>();
            _player = GetComponent<Player>();
            _spawnLocations = new()
            {
                { SpawnLocation.Default, _default },
                { SpawnLocation.RightHand, _rightHand },
                { SpawnLocation.LeftHand, _leftHand },
                { SpawnLocation.Weapon, _weapon }
            };
        }

        private void OnBasicCast()
        {
            if (_onCooldown || !_player.CanAct) return;

            var spellData = _spellBank.GetBasic();
            Cast(spellData);
        }

        private void OnCast(InputValue value)
        {
            if (_onCooldown || !_player.CanAct) return;

            var direction = value.Get<Vector2>();
            if (direction == Vector2.zero) return;

            var spellData = _spellBank.GetSpellInRotation(direction);
            Cast(spellData);
        }

        private void Cast(SpellData spellData)
        {
            if (!VerifyCanActivate(spellData)) return;

            var spell = SpellFactory.Get(spellData);
            spell.transform.SetParent(_spawnLocations[spell.Data.SpawnLocation], false);
            spell.transform.rotation = _player.transform.rotation;
            
            //if player takes damage, cancel the spell
            spell.Cast(_player);
            DisableCasting(spell.Data.CastTime);
            CooldownHandler.AddItem(spellData);
        }

        private bool VerifyCanActivate(SpellData data)
        {
            var onCooldown = CooldownHandler.GetCooldownTime(data.Name) > 0;
            return _player.CanAct && !onCooldown && _player.Stats.TryUseMana(data.ManaCost);
        }

        private async void DisableCasting(float duration)
        {
            IsCasting = true;
            await Awaitable.WaitForSecondsAsync(duration);
            IsCasting = false;
        }
    }
}