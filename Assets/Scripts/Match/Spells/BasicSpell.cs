using System.Collections;
using UnityEngine;

namespace Fighters.Match.Spells
{
    [RequireComponent(typeof(Rigidbody), typeof(BasicSpellData))]
    public class BasicSpell : MonoBehaviour, ISpell
    {
        [SerializeField] private GameObject _hitEffectPrefab;

        private BasicSpellData _data;

        public ISpellData Data => _data;

        private void Awake()
        {
            _data = GetComponent<BasicSpellData>();
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

        public void Cast()
        {

        }
    }
}