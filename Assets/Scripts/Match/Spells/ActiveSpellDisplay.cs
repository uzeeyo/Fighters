using System.Collections.Generic;
using UnityEngine;

namespace Fighters.Match.Spells
{
    public class ActiveSpellDisplay : MonoBehaviour
    {
        [SerializeField] private SpellIcon _spellIconUp;
        [SerializeField] private SpellIcon _spellIconDown;
        [SerializeField] private SpellIcon _spellIconLeft;
        [SerializeField] private SpellIcon _spellIconRight;

        private Dictionary<Vector2, SpellIcon> _spellIcons;

        private void Awake()
        {
            _spellIcons = new Dictionary<Vector2, SpellIcon>
            {
                {Vector2.up, _spellIconUp},
                {Vector2.down, _spellIconDown},
                {Vector2.left, _spellIconLeft},
                {Vector2.right, _spellIconRight}
            };
        }

        public void SetSpellIcons(ISpell[] spells)
        {

        }
    }
}