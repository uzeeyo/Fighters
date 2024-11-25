using System.Collections;
using Fighters.Match.Spells;
using UnityEngine;
using UnityEngine.UI;

namespace Fighters.UI
{
    public class SpellIcon : MonoBehaviour
    {
        private const float ANIMATION_DURATION = 0.2f;

        [SerializeField] private Image _spellIcon;
        [SerializeField] private Image _cooldownOverlay;
        [SerializeField] private Image _border;
        [SerializeField] private Image _extendedBorder;
        private Color _originalColor;

        private Color _cooldownColor = Color.red;

        private string _name;

        public string Name => _name;

        private void Awake()
        {
            _cooldownOverlay.fillAmount = 0;
            _originalColor = _border.color;
            _extendedBorder.color = _originalColor;
        }

        public void Load(SpellData data)
        {
            _name = data.Name;
            _spellIcon.sprite = data.Icon;
        }

        public void SetCooldown(float seconds)
        {
            StartCoroutine(StartCooldown(seconds));
            StartCoroutine(PlayActivationAnimation());
        }

        private IEnumerator StartCooldown(float seconds)
        {
            _cooldownOverlay.fillAmount = 1;
            while (_cooldownOverlay.fillAmount > 0)
            {
                _cooldownOverlay.fillAmount -= Time.deltaTime / seconds;
                yield return null;
            }
            _border.color = _originalColor;
            _extendedBorder.color = _originalColor;
            _cooldownOverlay.fillAmount = 0;
        }

        private IEnumerator PlayActivationAnimation()
        {
            float timeElapsed = 0;
            var extendedBorderTransform = _extendedBorder.transform;

            while (timeElapsed < ANIMATION_DURATION)
            {
                float progress = timeElapsed / ANIMATION_DURATION;
                _border.color = Color.Lerp(_originalColor, _cooldownColor, progress);
                _extendedBorder.color = Color.Lerp(_originalColor, _cooldownColor, progress);
                extendedBorderTransform.localScale = ActiveSpellDisplay.BorderResizeCurve.Evaluate(progress) * Vector3.one;

                timeElapsed += Time.deltaTime;
                yield return null;
            }
            _extendedBorder.color = _cooldownColor;
            _border.color = _cooldownColor;
            extendedBorderTransform.localScale = Vector3.one;

        }

    }
}