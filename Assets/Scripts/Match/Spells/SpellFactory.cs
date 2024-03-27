using System.Collections.Generic;
using UnityEngine;

namespace Fighters.Match.Spells
{
    public class SpellFactory : MonoBehaviour
    {
        [SerializeField] private DamageSpell _damageSpellPrefab;
        [SerializeField] private Spell _healSpellPrefab;
        [SerializeField] private Spell _buffSpellPrefab;

        private Dictionary<SpellType, Spell> _prefabs = new();

        private static SpellFactory _instance;

        public static SpellFactory Instance => _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _prefabs = new()
            {
                { SpellType.Damage, _damageSpellPrefab },

            };
        }

        public Spell Get(SpellData data)
        {
            var spell = Instantiate(_prefabs[data.SpellType]);
            spell.Init(data);
            return spell;
        }
    }
}