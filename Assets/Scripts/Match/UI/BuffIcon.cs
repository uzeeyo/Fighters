using System;
using System.Threading;
using Extensions;
using Fighters.Match.Spells;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Match.UI
{
    public class BuffIcon : MonoBehaviour
    {
        private TextMeshProUGUI _timeRemainingText;
        
        [SerializeField] private Image _icon;
        
        public BuffEffect Effect;

        private void Awake()
        {
            _timeRemainingText = GetComponentInChildren<TextMeshProUGUI>();
            _timeRemainingText.Outline(Color.black, 0.1f);
        }

        public void Init(BuffEffect effect)
        {
            Effect = effect;
            _icon.sprite = effect.Icon;
            Countdown();
        }

        
        //TODO: this should probably be removed and subscribe to a TimeRemainingChanged event
        private async void Countdown()
        {
            while (Effect.TimeRemaining >= 0 && gameObject)
            {
                _icon.fillAmount = Effect.TimeRemaining / Effect.Duration;
                _timeRemainingText.text = Effect.TimeRemaining.ToString("F2");
                await Awaitable.NextFrameAsync();
            }
            Destroy(gameObject);
        }
    }
}