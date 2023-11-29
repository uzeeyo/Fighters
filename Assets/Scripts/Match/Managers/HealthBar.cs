using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Fighters.Match
{
    public class HealthBar : MonoBehaviour
    {
        private Image _image;
        private float _maxHealth = 100;
        private float _currentHealth;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _currentHealth = _maxHealth;
        }

        public IEnumerator UpdateHealthBar(float delta = -5)
        {
            _currentHealth += delta;
            var fill = _currentHealth / _maxHealth;
            if (_currentHealth <= 0)
            {
                fill = 0;
                Debug.Log("DEAD");
            }

            while (_image.fillAmount > fill)
            {
                _image.fillAmount = Mathf.Lerp(_image.fillAmount, fill, Time.deltaTime * 10);
                yield return null;
            }

        }
    }
}
