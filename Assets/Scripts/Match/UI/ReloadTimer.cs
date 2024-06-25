using Fighters.Match.Spells;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Fighters.Match.UI
{
    public class ReloadTimer : MonoBehaviour
    {
        [SerializeField] private Image _timerBar;

        private SpellBank _spellBank;

        public void Init(SpellBank spellBank)
        {
            _spellBank = spellBank;
            _spellBank.SpellsReloaded += OnSpellsReloaded;
        }

        private void OnDisable()
        {
            _spellBank.SpellsReloaded -= OnSpellsReloaded;
        }

        private void OnSpellsReloaded()
        {
            StartCoroutine(Countdown());
        }

        private IEnumerator Countdown()
        {
            float timeElapsed = 0;
            float duration = SpellBank.RELOAD_COOLDOWN;

            while (timeElapsed < duration)
            {
                _timerBar.fillAmount = 1 - (timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            _timerBar.fillAmount = 1;
        }
    }
}