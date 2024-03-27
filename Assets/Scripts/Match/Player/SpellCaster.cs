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

        private void Awake()
        {
            _spellBank = GetComponent<SpellBank>();
            _player = GetComponent<Player>();
        }

        private IEnumerator StartCooldown(float seconds)
        {
            _onCooldown = true;
            yield return new WaitForSeconds(seconds);
            _onCooldown = false;
        }

        private void OnBasicCast()
        {
            if (_onCooldown) return;

            var basicSpell = Instantiate(_spellBank.BasicSpell, transform);
            basicSpell.Cast(_player.CurrentTile);
            StartCoroutine(StartCooldown(basicSpell.Cooldown));
        }

        private void OnCast(InputValue value)
        {
            if (_onCooldown) return;

            var direction = value.Get<Vector2>();
            if (direction == Vector2.zero) return;

            var spellData = _spellBank.GetSpellInRotation(direction);
            var spell = SpellFactory.Instance.Get(spellData);
            StartCoroutine(StartCooldown(spell.Cooldown));
        }
    }
}