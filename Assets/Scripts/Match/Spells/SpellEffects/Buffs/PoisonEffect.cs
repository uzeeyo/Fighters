using Fighters.Match.Players;
using Fighters.Match.Spells;
using UnityEngine;

namespace Fighters.Buffs
{
    public class PoisonEffect : BuffEffect
    {
        public PoisonEffect(BuffData buffData) : base(buffData)
        {
            _dps = buffData.HPPS;
            _duration = buffData.Duration;
        }

        private readonly float _dps;
        private readonly float _duration;
        private bool _active;

        public override async void Activate()
        {
            float timer = 0;
            _active = true;
            
            while (timer < _duration || _active)
            {
                if (_playerStats.CurrentHealth <= 0)
                {
                    Deactivate();
                    return;
                }
                var damageForFrame = _dps * Time.deltaTime;
                _playerStats.TakeDamage(damageForFrame);
                timer += Time.deltaTime;

                await Awaitable.NextFrameAsync();
            }
        }

        public override void Deactivate()
        {
            _active = false;
        }
    }
}