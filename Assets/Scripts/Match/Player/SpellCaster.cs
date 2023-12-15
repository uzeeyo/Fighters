using System.Collections;
using UnityEngine;

namespace Fighters.Match
{
    public class SpellCaster : MonoBehaviour
    {
        [SerializeField] private GameObject _spellPrefab;
        [SerializeField] private float _spellDistance = 5;

        private bool _onCooldown;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_onCooldown)
            {
                var position = transform.position;
                position.x += _spellDistance;
                position.y += 3;
                var spell = Instantiate(_spellPrefab, position, Quaternion.identity);
                var spellObj = spell.GetComponent<BasicSpell>();
                spell.GetComponent<Rigidbody>().velocity = new Vector3(spellObj.SpellSpeed, 0, 0);
                StartCoroutine(StartCooldown(spellObj.Cooldown));
            }
        }

        private IEnumerator StartCooldown(float seconds)
        {
            _onCooldown = true;
            yield return new WaitForSeconds(seconds);
            _onCooldown = false;
        }
    }
}