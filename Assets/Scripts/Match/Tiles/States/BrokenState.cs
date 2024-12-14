using Fighters.Match.Players;
using Fighters.Match.Spells;
using UnityEngine;

namespace Fighters.Match
{
    public class BrokenTileState : ITemporalTileState
    {
        public BrokenTileState(float duration)
        {
            Duration = duration;
        }
        
        public bool IsSteppable => false;
        public string ShaderProperty => "Broken";
        public float Duration { get; private set; }
        
    }
}