using UnityEngine;

namespace Fighters.Match
{
    public class MatchManager : MonoBehaviour
    {
        private static bool _matchStarted;

        public static bool MatchStarted { get => _matchStarted; }

        private void Start()
        {
            _matchStarted = true;
        }
    }
}