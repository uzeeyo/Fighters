using System;
using System.Threading;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Match.UI
{
    public class BuffIcon : MonoBehaviour
    {
        private float _duration;
        private TextMeshProUGUI _timeRemainingText;
        
        [SerializeField] private Image _icon;

        private void Awake()
        {
            _timeRemainingText = GetComponentInChildren<TextMeshProUGUI>();
            _timeRemainingText.Outline(Color.black, 0.1f);
        }

        public void Init(Sprite icon, float duration)
        {
            _icon.sprite = icon;
            _duration = duration;
            Countdown();
        }

        private async void Countdown()
        {
            float timer = 0f;
            float startTime = Time.time;

            while (timer < _duration)
            {
                _icon.fillAmount = timer / _duration;
                _timeRemainingText.text = (Time.time - startTime).ToString("F2");
                timer += Time.deltaTime;
                await Awaitable.NextFrameAsync();
            }
            Destroy(gameObject);
        }
    }
}