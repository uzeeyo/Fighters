using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Fighters.Match.Spells
{
    public class SpellBank : MonoBehaviour
    {
        public const float RELOAD_COOLDOWN = 10f;

        private bool _reloadOnCooldown;
        private List<SpellData> _spells;
        private Dictionary<Vector2, SpellData> _activeSpells;
        //TODO: Move back to cooldown handler
        private List<CooldownItem> _itemsOnCooldown = new List<CooldownItem>();

        [SerializeField] private SpellData _basicSpell;

        public Dictionary<Vector2, SpellData> ActiveSpells => _activeSpells;

        public event Action<Dictionary<Vector2, SpellData>> SpellsChanged;
        public event Action<CooldownItem> CooldownChanged;
        public event Action SpellsReloaded;

        public SpellData GetBasic()
        {
            return _basicSpell;
        }

        public SpellData GetSpellInRotation(Vector2 direction)
        {
            return _activeSpells[direction];
        }

        public void ReloadActiveSpells()
        {
            var spellData = _spells.OrderBy(x => Guid.NewGuid()).Take(4).ToList();
            var directions = new Vector2[]
            {
                Vector2.up,
                Vector2.right,
                Vector2.down,
                Vector2.left
            };

            _activeSpells = new Dictionary<Vector2, SpellData>();
            for (int i = 0; i < 4; i++)
            {
                _activeSpells[directions[i]] = spellData[i];
            }
            SpellsChanged?.Invoke(_activeSpells);
            SpellsReloaded?.Invoke();
        }

        public void LoadSpells(List<SpellData> spells)
        {
            _spells = spells;
            ReloadActiveSpells();
        }

        private void OnReload()
        {
            if (_reloadOnCooldown) return;
            StartCoroutine(StartReloadCooldown());
        }

        private IEnumerator StartReloadCooldown()
        {
            ReloadActiveSpells();

            _reloadOnCooldown = true;
            yield return new WaitForSeconds(RELOAD_COOLDOWN);
            _reloadOnCooldown = false;
        }

        public void StartSpellCooldown(SpellData data)
        {
            var item = new CooldownItem(data.Name, data.Cooldown);
            _itemsOnCooldown.Add(item);
            StartCoroutine(CountDown(item));
            CooldownChanged?.Invoke(item);
        }

        public float GetCooldownTime(string spellName)
        {
            return _itemsOnCooldown.Find(x => x.Name == spellName)?.TimeRemaining ?? 0;
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