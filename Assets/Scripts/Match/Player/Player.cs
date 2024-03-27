using Fighters.Contestants;
using Fighters.Match.Spells;
using System.Collections.Generic;
using UnityEngine;

namespace Fighters.Match.Players
{
    public class Player : MonoBehaviour
    {
        //TODO: Auto assign this
        [SerializeField] private TileGrid _grid;

        private PlayerStats _stats;
        private SpellBank _spellBank;

        public PlayerStats Stats => _stats;
        public SpellBank SpellBank => _spellBank;
        public TileGrid Grid => _grid;
        public Tile CurrentTile { get; set; }


        private void Awake()
        {
            _stats = GetComponent<PlayerStats>();
            _spellBank = GetComponent<SpellBank>();
        }

        public void Init(StatData statData, List<SpellData> spells)
        {
            _stats.Init(statData);
            _spellBank.LoadSpells(spells);
        }

        public void SetLocation(Vector2 location)
        {

        }
    }
}