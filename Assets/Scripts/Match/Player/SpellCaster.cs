using Fighters.Match.Players;
using Fighters.Match.Spells;
using System.Collections;
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

        private void Awake()
        {
            _spellBank = GetComponent<SpellBank>();
            _player = GetComponent<Player>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void OnBasicCast()
        {
            if (_onCooldown || !_player.CanInteract) return;

            var spellData = _spellBank.GetBasic();
            if (!VerifyCanActivate(spellData)) return;

            var spell = SpellFactory.Instance.Get(spellData);
            spell.transform.rotation = _player.transform.rotation;

            StartCoroutine(spell.Cast(_player.CurrentTile));
            StartCoroutine(DisableInteractionsWhileCasting(spell.CastTime));
            _spellBank.StartSpellCooldown(spellData);
            _animator.SetTrigger(spellData.AnimationTriggerName);
        }

        private void OnCast(InputValue value)
        {
            if (_onCooldown || !_player.CanInteract) return;

            var direction = value.Get<Vector2>();
            if (direction == Vector2.zero) return;

            var spellData = _spellBank.GetSpellInRotation(direction);
            if (!VerifyCanActivate(spellData)) return;

            var spell = SpellFactory.Instance.Get(spellData);
            spell.transform.rotation = _player.transform.rotation;
            StartCoroutine(spell.Cast(_player.CurrentTile));
            StartCoroutine(DisableInteractionsWhileCasting(spell.CastTime));
            _spellBank.StartSpellCooldown(spellData);
            _animator.SetTrigger(spellData.AnimationTriggerName);
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