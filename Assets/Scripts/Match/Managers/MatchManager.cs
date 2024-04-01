using System;
using System.Collections;
using UnityEngine;

namespace Fighters.Match
{
    public enum PlayerSide
    {
        PlayerA,
        PlayerB
    }

    public class MatchManager : MonoBehaviour
    {
        private static bool _matchStarted;
        private static float _timeRemaining;
        private static bool _matchEnded;

        public static event Action<float> TimeRemainingChanged;
        public static event Action MatchEnded;

        public static bool MatchStarted { get => _matchStarted; }
        public static float TimeRemaining
        {
            get => _timeRemaining;
            private set
            {
                _timeRemaining = value;
                TimeRemainingChanged?.Invoke(_timeRemaining);
            }
        }

        private void Start()
        {
            StartMatch();
        }

        private IEnumerator Countdown()
        {
            while (_timeRemaining > 0 && !_matchEnded)
            {
                yield return new WaitForSeconds(1);
                TimeRemaining--;
            }
            EndMatch();
        }

        private void StartMatch()
        {
            TimeRemaining = 60;
            StartCoroutine(Countdown());
            _matchStarted = true;
        }

        private static void EndMatch()
        {
            Debug.Log("Match Ended");
            _matchEnded = true;
            MatchEnded?.Invoke();
        }

        private void DetermineWinner()
        {

        }
    }
}