using System.Collections;
using UnityEngine;

namespace Fighters.Match
{
    public class SpellCaster : MonoBehaviour
    {
        private bool _onCooldown;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var ani = GetComponentInChildren<IAttackPlayerAnimation>();
                ani.Play();
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