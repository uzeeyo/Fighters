using Fighters.Contestants;
using Fighters.Match.Spells;
using System.Collections.Generic;
using UnityEngine;

namespace Fighters.Match.Players
{
    [RequireComponent(typeof(PlayerStats), typeof(SpellBank))]
    public class Player : MonoBehaviour
    {
        //TODO: Auto assign this
        [SerializeField] private TileGrid _grid;


        public bool CanInteract { get; set; } = true;
        public PlayerStats Stats { get; private set; }
        public SpellBank SpellBank { get; private set; }
        public MovementController MovementController { get; private set; }
        public TileGrid Grid => _grid;
        public Tile CurrentTile { get; set; }
        //TODO: Auto assign 
        [field: SerializeField] public Side Side { get; private set; }


        private void Awake()
        {
            Stats = GetComponent<PlayerStats>();
            SpellBank = GetComponent<SpellBank>();
            MovementController = GetComponent<MovementController>();
        }

        public void Init(StatData statData, List<SpellData> spells)
        {
            Stats.Init(statData);
            SpellBank?.LoadSpells(spells);
        }

        public void SetLocation(Vector2 location)
        {

        }
    }
}