using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Fighters.Match.Spells
{
    public class SpellBank : MonoBehaviour
    {
        private List<SpellData> _spells;
        private Dictionary<Vector2, SpellData> _activeSpells;
        [SerializeField] private SpellData _basicSpell;

        public Dictionary<Vector2, SpellData> ActiveSpells => _activeSpells;

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
            var directions = new List<Vector2>
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
        }

        public void LoadSpells(List<SpellData> spells)
        {
            _spells = spells;
            ReloadActiveSpells();
        }
    }
}