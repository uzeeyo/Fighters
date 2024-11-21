using Fighters.Match.Players;
using Fighters.Match.Spells;
using System.Collections;
using System.Collections.Generic;
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
        private Animator _animator;

        private Dictionary<SpawnLocation, Transform> _spawnLocations;

        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _weapon;

        private void Awake()
        {
            _spellBank = GetComponent<SpellBank>();
            _player = GetComponent<Player>();
            _animator = GetComponentInChildren<Animator>();
            _spawnLocations = new()
            {
                { SpawnLocation.Default, transform },
                { SpawnLocation.RightHand, _rightHand },
                { SpawnLocation.LeftHand, _leftHand },
                { SpawnLocation.Weapon, _weapon }
            };
        }

        private void OnBasicCast()
        {
            if (_onCooldown || !_player.CanInteract) return;

            var spellData = _spellBank.GetBasic();
            Cast(spellData);
        }

        private void OnCast(InputValue value)
        {
            if (_onCooldown || !_player.CanInteract) return;

            var direction = value.Get<Vector2>();
            if (direction == Vector2.zero) return;

            var spellData = _spellBank.GetSpellInRotation(direction);
            Cast(spellData);
        }

        public async void Cast(SpellData spellData)
        {
            if (!VerifyCanActivate(spellData)) return;

            var spell = SpellFactory.Get(spellData);
            spell.transform.SetParent(_spawnLocations[spell.Data.SpawnLocation]);
            spell.transform.rotation = _player.transform.rotation;
            //if player takes damage here, cancel the spell
            
            spell.Cast(_player);
            StartCoroutine(DisableInteractionsWhileCasting(spell.Data.CastTime));
            _spellBank.StartSpellCooldown(spellData);
        }

        private IEnumerator DisableInteractionsWhileCasting(float seconds)
        {
            _player.CanInteract = false;
            yield return new WaitForSeconds(seconds);
            _player.CanInteract = true;
        }

        private bool VerifyCanActivate(SpellData data)
        {
            var onCooldown = _spellBank.GetCooldownTime(data.Name) > 0;
            return _player.CanInteract && !onCooldown && _player.Stats.TryUseMana(data.ManaCost);
        }
    }
}