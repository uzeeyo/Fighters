using System.Collections;
using UnityEngine;

namespace Fighters.Match
{
    public class BasicSpell : MonoBehaviour, IDamageSpell
    {
        [SerializeField] private float _coolDown = 0.3f;
        [SerializeField] private float _spellSpeed = 20;
        [SerializeField] private float _damage = 5;
        [SerializeField] private GameObject _hitEffectPrefab;

        public float Cooldown { get => _coolDown; }

        public float SpellSpeed { get => _spellSpeed; }
        public float Damage { get => _damage; }

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
    }
}