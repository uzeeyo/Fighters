using UnityEngine;

namespace Fighters.Match
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private TileGrid _tileGrid;
        [SerializeField] private HealthBar _healthBar;

        private float _currentHealth;
        private float _maxHealth = 100;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public TileGrid TileGrid { get => _tileGrid; }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageSpell spell))
            {
                TakeDamage(spell.Damage);
            }
        }

        private void TakeDamage(float damage)
        {
            if (_currentHealth == 0)
            {
                return;
            }

            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                Debug.Log("Player died");
            }

            StartCoroutine(_healthBar.UpdateHealthBar(_currentHealth / _maxHealth));
        }
    }
}