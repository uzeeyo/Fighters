using UnityEngine;

namespace Fighters.Match
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private TileGrid _tileGrid;
        [SerializeField] private HealthBar _healthBar;

        public TileGrid TileGrid { get => _tileGrid; }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Spell spell))
            {
                StartCoroutine(_healthBar.UpdateHealthBar(spell.Damage));
            }
        }
    }
}