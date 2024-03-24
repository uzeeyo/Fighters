using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Fighters.Match
{
    public class SpellBank : MonoBehaviour
    {
        private List<ISpell> _spells;
        private Dictionary<Vector2, ISpell> _activeSpells;

        public Dictionary<Vector2, ISpell> ActiveSpells => _activeSpells;

        public void LoadSpells(List<ISpell> spells)
        {
            _spells = spells;
        }

        public List<ISpell> GetRandomSpells(int amount = 1)
        {
            return _spells.OrderBy(spell => Guid.NewGuid()).Take(amount).ToList();
        }

        public void DeleteSpell(ISpell spell)
        {
            //TODO: I'm sure this doesn't work
            _spells.Remove(spell);
        }
    }
}