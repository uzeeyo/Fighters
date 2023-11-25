using UnityEngine;

namespace Fighters.Match
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private TileGrid _tileGrid;

        public TileGrid TileGrid { get => _tileGrid; }

        //public override void OnNetworkSpawn()
        //{
        //    base.OnNetworkSpawn();
        //}
    }
}