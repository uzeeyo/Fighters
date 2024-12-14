using Fighters.Match.Players;
using UnityEngine;
namespace Fighters.Match
{
    public interface IBuffTileState : ITemporalTileState
    {
        void HandleStep(Player player);
    }
}