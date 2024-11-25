using System.Collections.Generic;
using Fighters.Match;
using Fighters.Match.Spells;
using UnityEngine;

namespace Fighters.UI
{
    public class ActiveSpellDisplay : MonoBehaviour
    {
        [SerializeField] private SpellIcon _spellIconUp;
        [SerializeField] private SpellIcon _spellIconDown;
        [SerializeField] private SpellIcon _spellIconLeft;
        [SerializeField] private SpellIcon _spellIconRight;
        [SerializeField] private AnimationCurve _borderResizeCurve;

        private SpellBank _spellBank;
        private CooldownHandler _cooldownHandler;

        public static AnimationCurve BorderResizeCurve { get; private set; }

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
            BorderResizeCurve = _borderResizeCurve;
        }

        public void Init(SpellBank spellBank, CooldownHandler cooldownHandler)
        {
            _spellBank = spellBank;
            _spellBank.SpellsChanged += OnSpellsChanged;
            
            _cooldownHandler = cooldownHandler;
            _cooldownHandler.CooldownItemsChanged += OnCooldownChanged;
        }

        private void OnDisable()
        {
            _spellBank.SpellsChanged -= OnSpellsChanged;
            _cooldownHandler.CooldownItemsChanged -= OnCooldownChanged;
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