using System;
using System.Collections;
using Fighters.Match.Players;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fighters.Match
{
    public enum Side
    {
        Self,
        Opponent,
        Both
    }

    public class MatchManager : MonoBehaviour
    {
        const float MATCH_DURATION = 180f;

        private static float _timeRemaining;
        private static bool _matchEnded;
        
        [SerializeField] private TileGrid _grid;

        public static event Action<float> TimeRemainingChanged;
        public static event Action MatchEnded;
        public static bool MatchStarted { get; private set; }

        public static TileGrid Grid { get; private set; }

        public static float TimeRemaining
        {
            get => _timeRemaining;
            private set
            {
                _timeRemaining = value;
                TimeRemainingChanged?.Invoke(_timeRemaining);
            }
        }

        private void Awake()
        {
            MatchStarted = false;
            Grid = _grid;
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

        public void StartMatch()
        {
            TimeRemaining = MATCH_DURATION;
            StartCoroutine(Countdown());
            MatchStarted = true;
        }

        private static void EndMatch()
        {
            Debug.Log("Match Ended");
            _matchEnded = true;
            MatchEnded?.Invoke();
        }

        private void OnDestroy()
        {
            Grid = null;
        }
    }
}