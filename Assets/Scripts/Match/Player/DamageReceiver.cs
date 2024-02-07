using UnityEngine;

namespace Fighters.Match.Player
{
    public class DamageReceiver : MonoBehaviour
    {
        private PlayerStats _stats;

        private void Awake()
        {
            _stats = GetComponent<PlayerStats>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageSpell spell))
            {
                _stats.TakeDamage(spell.Damage);
            }
        }
    }
}