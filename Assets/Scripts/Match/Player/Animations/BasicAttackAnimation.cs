using Fighters.Match;
using UnityEngine;

namespace Fighters.Utilities
{
    public class BasicAttackAnimation : MonoBehaviour, IAttackPlayerAnimation
    {
        private Animator _animator;

        [SerializeField] private GameObject _spellPrefab;
        [SerializeField] private float _spellDistance = 5;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnProjectileFire()
        {
            var position = transform.position;
            position.x += _spellDistance;
            position.y += 3;
            var spell = Instantiate(_spellPrefab, position, Quaternion.identity);
            var spellObj = spell.GetComponent<BasicSpell>();
            spell.GetComponent<Rigidbody>().velocity = new Vector3(spellObj.SpellSpeed, 0, 0);
            //StartCoroutine(StartCooldown(spellObj.Cooldown));
        }

        public void Play()
        {
            _animator.SetTrigger("Punch");
        }
    }
}