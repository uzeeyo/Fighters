using Fighters.Match.Spells;
using UnityEngine;

namespace Fighters.Buffs
{
    public class RootEffect : BuffEffect
    {
        public RootEffect(BuffData data) : base(data)
        { }
        
        public override void Activate()
        {
            _playerStats.DisableMovementEffects.Add(this);
        }

        public override void Deactivate()
        {
            _playerStats.DisableMovementEffects.Remove(this);
        }
    }
}