using System.Collections;
using UnityEngine;

namespace Fighters.Match.Spells
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlasmaShot : Spell
    {
        private float _damage;
        private float _travelTime;
        private Vector3 _originalPosition;
        AnimationCurve _travelCurve;

        public float Damage => _damage;

        public override void Init(SpellData data)
        {
            base.Init(data);
            _travelTime = data.TravelTime;
            _travelCurve = data.HorizantalCurve;
        }

        public override IEnumerator Cast(Tile origin)
        {
            //play cast animation
            _originalPosition = origin.transform.position;
            _originalPosition.y = 1;
            _originalPosition.x = _originalPosition.x + 1.5f;

            transform.position = _originalPosition;
            yield return new WaitForSeconds(CastTime);
            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            var timeElapsed = 0f;
            var duration = _travelTime;

            var maxX = _originalPosition.x + MAX_TRAVEL_DISTANCE;
            var targetPosition = new Vector3(maxX, _originalPosition.y, _originalPosition.z);


            while (timeElapsed < duration)
            {
                var horizantalPercentage = _travelCurve.Evaluate(timeElapsed / duration);
                transform.position = _originalPosition + (targetPosition - _originalPosition) * horizantalPercentage;

                timeElapsed += Time.deltaTime;
                yield return null;
            }

            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
        }

    }
}