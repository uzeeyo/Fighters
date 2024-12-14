using Fighters.Match.Players;

namespace Fighters.Match
{
    public interface ITileState
    {
        bool IsSteppable { get; }
        string ShaderProperty { get; }
    }
}