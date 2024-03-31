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

        public void OnCooldownChanged(CooldownItem cooldownItem)
        {
            foreach (var item in _spellIcons)
            {
                if (item.Value.Name == cooldownItem.Name)
                {
                    item.Value.SetCooldown(cooldownItem.TimeRemaining);
                }
            }
        }

        public void OnSpellsChanged(Dictionary<Vector2, SpellData> spellData)
        {
            foreach (var direction in spellData.Keys)
            {
                var icon = _spellIcons[direction];
                icon.Load(spellData[direction]);
            }
        }
    }
}