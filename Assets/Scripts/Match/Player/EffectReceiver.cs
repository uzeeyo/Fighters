using Fighters.Match.Spells;
using UnityEngine;

namespace Fighters.Match.Players
{
    public class EffectReceiver : MonoBehaviour
    {
        private PlayerStats _stats;

        private void Awake()
        {
            _stats = GetComponent<PlayerStats>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out SpellEffect effect))
            {
                effect.Apply(_stats);
            }
        }
    }
}