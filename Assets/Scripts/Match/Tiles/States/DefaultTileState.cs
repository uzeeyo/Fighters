using Fighters.Match.Players;

namespace Fighters.Match
{
    public class DefaultTileState : ITileState
    {
        public bool IsSteppable => true;
        public string ShaderProperty => "Default";
    }
}