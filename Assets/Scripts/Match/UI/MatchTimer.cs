using TMPro;
using UnityEngine;

namespace Fighters.Match
{
    public class MatchTimer : MonoBehaviour
    {
        private TextMeshProUGUI _timeLabel;
        private bool _criticalTimeReached;

        private void Awake()
        {
            _timeLabel = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _timeLabel.text = MatchManager.TimeRemaining.ToString();
            MatchManager.TimeRemainingChanged += OnTimeRemainingChanged;
        }

        private void OnDisable()
        {
            MatchManager.TimeRemainingChanged -= OnTimeRemainingChanged;
        }

        private void OnTimeRemainingChanged(float timeRemaining)
        {
            if (timeRemaining < 10 && !_criticalTimeReached)
            {
                _timeLabel.color = Color.red;
                _criticalTimeReached = true;
            }

            var seconds = Mathf.FloorToInt(timeRemaining);
            _timeLabel.text = seconds.ToString();
        }

        private void Reset()
        {
            _timeLabel.color = Color.white;
            _criticalTimeReached = false;
        }
    }
}