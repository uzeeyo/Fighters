using System.Collections.Generic;
using UnityEngine;

namespace Fighters.Match
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private TileGrid[] _grids;
        [SerializeField] private StatusBar _healthBarA;
        [SerializeField] private StatusBar _healthBarB;
        [SerializeField] private StatusBar _manaBarA;
        [SerializeField] private StatusBar _manaBarB;

        private Dictionary<Side, (StatusBar, StatusBar)> _playerStatusBarMap;

        private void Awake()
        {
            _playerStatusBarMap = new Dictionary<Side, (StatusBar, StatusBar)>
            {
                { Side.PlayerA, (_healthBarA, _manaBarA) },
                { Side.PlayerB, (_healthBarB, _manaBarB) }
            };
        }

        public void SpawnPlayer(Side side)
        {

        }
    }
}