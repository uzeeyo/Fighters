using System;
using UnityEngine;

namespace Fighters.Match.Spells
{
    public class Projectile : SpellVisual
    {
        private Spell _spell;

        protected override void Awake()
        {
            base.Awake();
            _spell = GetComponentInParent<Spell>();
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            _spell.OnImpact(this, other, transform.position);
        }

        public async void MoveToPosition(Vector3 targetPosition)
        {
            float timer = 0;
            var originalPosition = transform.position;

            while (gameObject && timer < _spell.Data.TravelTime)
            {
                timer += Time.deltaTime;
                var percentage = timer / _spell.Data.TravelTime;
                var distancePercentage = _spell.Data.SpeedCurve.Evaluate(percentage);
                var newPosition =
                    originalPosition + (targetPosition - originalPosition) * distancePercentage;
                newPosition.y += _spell.Data.VerticalCurve.Evaluate(percentage);
                newPosition.x += _spell.Data.HorizontalCurve.Evaluate(percentage);
                transform.position = newPosition;

                try
                {
                    await Awaitable.NextFrameAsync(destroyCancellationToken);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
            transform.position = targetPosition;

            _spell.OnImpact(this, null, targetPosition);
        }
    }
}