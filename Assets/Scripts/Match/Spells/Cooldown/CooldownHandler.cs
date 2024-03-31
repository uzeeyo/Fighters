using Fighters.Match.Spells;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fighters.Match
{
    public class CooldownHandler : MonoBehaviour
    {
        private List<CooldownItem> _itemsOnCooldown = new List<CooldownItem>();

        public void AddItem(SpellData spellData)
        {
            var item = new CooldownItem(spellData.Name, spellData.Cooldown);

            _itemsOnCooldown.Add(item);
            StartCoroutine(CountDown(item));
        }

        public float GetCooldownTime(string name)
        {

            if (_itemsOnCooldown.Exists(x => x.Name == name))
            {
                return _itemsOnCooldown.Find(x => x.Name == name).TimeRemaining;
            }
            return 0;
        }

        private IEnumerator CountDown(CooldownItem item)
        {
            float timeElapsed = 0;
            float duration = item.TimeRemaining;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                item.TimeRemaining = duration - timeElapsed;
                yield return null;
            }
            _itemsOnCooldown.Remove(item);
        }
    }
}