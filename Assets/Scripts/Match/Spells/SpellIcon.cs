using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Fighters.Match.Spells
{
    public class SpellIcon : MonoBehaviour
    {
        [SerializeField] private Image _spellIcon;
        [SerializeField] private Image _cooldownOverlay;

        private string _name;

        public string Name => _name;

        private void Awake()
        {
            _cooldownOverlay.fillAmount = 0;
        }

        public void Load(SpellData data)
        {
            _name = data.Name;
            _spellIcon.sprite = data.Icon;
        }

        public void SetCooldown(float seconds)
        {
            StartCoroutine(StartCooldown(seconds));
        }

        private IEnumerator StartCooldown(float seconds)
        {
            _cooldownOverlay.fillAmount = 1;
            while (_cooldownOverlay.fillAmount > 0)
            {
                _cooldownOverlay.fillAmount -= Time.deltaTime / seconds;
                yield return null;
            }
            _cooldownOverlay.fillAmount = 0;
        }

    }
}