using UnityEngine;

namespace Fighters.Match.Players
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
            if (collision.gameObject.TryGetComponent(out Spell spellData))
            {
                //_stats.TakeDamage(spellData.);
            }
        }
    }
}