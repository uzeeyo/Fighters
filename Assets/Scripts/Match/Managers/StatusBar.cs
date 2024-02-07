using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Fighters.Match
{
    public class StatusBar : MonoBehaviour
    {
        private bool _isUpdating = false;
        private float _targetPercent;

        [SerializeField] private Image _barImage;

        public IEnumerator UpdateBar(float percent)
        {
            _targetPercent = percent;
            if (_isUpdating)
            {
                yield break;
            }

            _isUpdating = true;
            while (_targetPercent / _barImage.fillAmount > 0.05f)
            {
                _barImage.fillAmount = Mathf.Lerp(_barImage.fillAmount, _targetPercent, Time.deltaTime * 15);
                yield return null;
            }
            _barImage.fillAmount = _targetPercent;
            _isUpdating = false;
        }
    }
}
