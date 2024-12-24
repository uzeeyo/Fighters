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

        //TODO: Remove test data ref
        [SerializeField] private SpellData _basicSpell;

        public event Action<Dictionary<Vector2, SpellData>> SpellsChanged;
        public event Action SpellsReloaded;

        public SpellData GetBasic()
        {
            return _basicSpell;
        }

        public SpellData GetSpellInRotation(Vector2 direction)
        {
            return _activeSpells[direction];
        }

        private void ReloadActiveSpells()
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

        public SpellData GetRandomSpell()
        {
            return _spells.OrderBy(x => Guid.NewGuid()).Take(1).Single();
        }

        private void OnReload()
        {
            if (_reloadOnCooldown) return;
            StartReloadCooldown();
        }

        private async void StartReloadCooldown()
        {
            ReloadActiveSpells();

            _reloadOnCooldown = true;
            await Awaitable.WaitForSecondsAsync(RELOAD_COOLDOWN);
            _reloadOnCooldown = false;
        }
    }
}