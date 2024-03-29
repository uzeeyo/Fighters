using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fighters.Match.Spells
{
    public class SpellFactory : MonoBehaviour
    {
        private Dictionary<SpellType, Type> _spellTypes;

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

            _spellTypes = new()
            {
                { SpellType.Damage, typeof(DamageEffect) },
            };
        }

        public Spell Get(SpellData data)
        {
            var spell = Instantiate(data.Prefab);
            spell.Init(data);
            spell.gameObject.AddComponent(_spellTypes[data.SpellType]);
            spell.GetComponent<SpellEffect>().SetData(data);

            return spell;
        }
    }
}