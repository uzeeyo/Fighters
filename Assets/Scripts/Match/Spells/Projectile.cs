using System;
using UnityEngine;

namespace Fighters.Match.Spells
{
    public class Projectile : SpellVisual
    {
        public enum RotationMode
        {
            None,
            FollowVelocity
        }
        
        private Spell _spell;
        private Tile _targetTile;

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

        public void MoveToTile(Tile tile)
        {
            _targetTile = tile;
            MoveToPosition(tile.transform.position, RotationMode.FollowVelocity);
        }

        public async void MoveToPosition(Vector3 targetPosition, RotationMode rotationMode = RotationMode.None)
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
                
                if (rotationMode == RotationMode.FollowVelocity) transform.forward = (newPosition - transform.position).normalized;
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
            if (_targetTile != null)
            {
                _targetTile.ChangeState(_spell.Data);
            }

            var hitPosition = targetPosition;
            hitPosition.y += 0.5f;

            _spell.OnImpact(this, null, hitPosition);
        }
    }
}