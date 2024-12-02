using System;
using UnityEngine;
using UnityEngine.VFX;

namespace Fighters.Match.Spells
{
    public class Projectile : MonoBehaviour
    {
        private Spell _spell;
        private VisualEffect _visualEffect;

        private void Awake()
        {
            _spell = GetComponentInParent<Spell>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _spell.OnImpact(other, transform.position, gameObject);
        }

        public async void MoveToPosition(Vector3 targetPosition, float travelTime, AnimationCurve speedCurve)
        {
            float timer = 0;
            var originalPosition = transform.position;

            while (gameObject && timer < travelTime)
            {
                var curveValue = speedCurve.Evaluate(timer / travelTime);
                transform.position = originalPosition + (targetPosition - originalPosition) * curveValue;

                timer += Time.deltaTime;
                try
                {
                    await Awaitable.NextFrameAsync(destroyCancellationToken);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }

            _spell.OnImpact(null, targetPosition, gameObject);
        }
    }
}