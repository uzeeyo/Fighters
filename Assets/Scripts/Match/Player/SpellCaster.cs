using Fighters.Match.Players;
using Fighters.Match.Spells;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

namespace Fighters.Match
{
    [RequireComponent(typeof(SpellBank), typeof(PlayerInput))]
    public class SpellCaster : MonoBehaviour
    {
        private static readonly int Cast = Animator.StringToHash("Cast");
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
            StartCast(spellData);
        }

        private void OnCast(InputValue value)
        {
            if (_onCooldown || !_player.CanAct) return;

            var direction = value.Get<Vector2>();
            if (direction == Vector2.zero) return;

            var spellData = _spellBank.GetSpellInRotation(direction);
            StartCast(spellData);
        }

        public float CastRandom()
        {
            var spellData = _spellBank.GetRandomSpell();
            StartCast(spellData);
            return _player.AnimationHandler.GetAnimationTime(spellData.AnimationName);
        }

        private async void StartCast(SpellData spellData)
        {
            if (!VerifyCanActivate(spellData)) return;
            
            var spell = SpellFactory.Get(spellData);
            SetSpellTransform(spell);
            
            //start cooldown immediately or after spell finishes??
            CooldownHandler.AddItem(spell.Data);
            
            var animationTime = _player.AnimationHandler.Play(spell.Data.AnimationName);
            PlayVFX(spell, "OnCast");
            
            DisableCasting(animationTime);
            await Awaitable.WaitForSecondsAsync(spell.Data.CastTime, spell.destroyCancellationToken);
            
            LaunchSpell(spell);
        }

        private void LaunchSpell(Spell spell)
        {
            PlayVFX(spell, "OnLaunch");
            if (spell.TryGetComponent(out Animator animator))
            {
                animator.SetTrigger(Cast);
            }
            if (spell.Data.ShakesOnCast) Shaker.Shake(spell.Data.ShakeStrength, spell.Data.ShakeDuration);
            
            Targeter.Target(_player, spell);
        }

        private static void PlayVFX(Spell spell, string eventName)
        {
            foreach (var vfx in spell.GetComponentsInChildren<VisualEffect>())
            {
                vfx.SendEvent(eventName);
            }
        }

        private async void DisableCasting(float duration)
        {
            IsCasting = true;
            await Awaitable.WaitForSecondsAsync(duration, destroyCancellationToken);
            IsCasting = false;
        }

        private void SetSpellTransform(Spell spell)
        {
            var spawnParent = _spawnLocations[spell.Data.SpawnLocation];
            if (spawnParent == null)
            {
                Debug.LogWarning("No spawn location specified for spell: " + spell.Data.Name);
                spawnParent = transform;
            }
            spell.transform.SetParent(spawnParent, false);
            spell.transform.rotation = _player.transform.rotation;
        }

        private bool VerifyCanActivate(SpellData data)
        {
            var onCooldown = CooldownHandler.GetCooldownTime(data.Name) > 0;
            return _player.CanAct && !onCooldown && _player.Stats.TryUseMana(data.ManaCost);
        }
    }
}