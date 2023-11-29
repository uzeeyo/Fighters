using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Fighters.Match
{
    public class HealthBar : MonoBehaviour
    {
        private Image _image;
        private bool _isUpdating = false;
        private float _targetPercent;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public IEnumerator UpdateHealthBar(float percentHealth)
        {
            _targetPercent = percentHealth;
            if (_isUpdating)
            {
                yield break;
            }

            _isUpdating = true;
            while ((_targetPercent / _image.fillAmount) > 0.05f)
            {
                _image.fillAmount = Mathf.Lerp(_image.fillAmount, _targetPercent, Time.deltaTime * 15);
                yield return null;
            }
            _image.fillAmount = _targetPercent;
            _isUpdating = false;
        }
    }
}
