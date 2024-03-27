using System.Collections;
using UnityEngine;

namespace Fighters.Match.Spells
{
    [RequireComponent(typeof(Rigidbody))]
    public class BasicSpell : Spell
    {
        [SerializeField] private GameObject _hitEffectPrefab;

        private void Awake()
        {
            StartCoroutine(DestroyAfterSeconds(2));
        }

        private IEnumerator DestroyAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var hitEffect = Instantiate(_hitEffectPrefab, transform.position, Quaternion.identity);
            hitEffect.GetComponent<IHitEffect>().Play();
            Destroy(gameObject);
        }

        public override void Cast(Tile origin)
        {
            var startPosition = origin.transform.position;
            startPosition.y = 1f;
            transform.position = startPosition;

            CastForward();
        }
    }
}