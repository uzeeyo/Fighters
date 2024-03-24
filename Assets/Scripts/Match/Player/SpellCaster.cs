using System.Collections;
using UnityEngine;

namespace Fighters.Match
{
    [RequireComponent(typeof(SpellBank))]
    public class SpellCaster : MonoBehaviour
    {
        private bool _onCooldown = false;
        private SpellBank _spellBank;

        private void Awake()
        {
            _spellBank = GetComponent<SpellBank>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _spellBank.ActiveSpells[Vector2.up].Cast();
            }

        }

        private IEnumerator StartCooldown(float seconds)
        {
            _onCooldown = true;
            yield return new WaitForSeconds(seconds);
            _onCooldown = false;
        }
    }
}