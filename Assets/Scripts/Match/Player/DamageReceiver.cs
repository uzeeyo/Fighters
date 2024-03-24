using Fighters.Match.Spells;
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
            if (collision.gameObject.TryGetComponent(out IDamageData spellData))
            {
                _stats.TakeDamage(spellData.Damage);
            }
        }
    }
}