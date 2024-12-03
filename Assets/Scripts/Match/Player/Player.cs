using Fighters.Contestants;
using Fighters.Match.Spells;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fighters.Match.UI;
using Fighters.UI;
using Match.Player;
using Match.UI;
using UnityEngine;
using UnityEngine.VFX;

namespace Fighters.Match.Players
{
    [RequireComponent(typeof(PlayerStats), typeof(SpellBank))]
    public class Player : MonoBehaviour
    {
        private SpellBank _spellBank;
        private SpellCaster _spellCaster;
        
        public bool CanAct => !_spellCaster.IsCasting && !Stats.DisableActionEffects.Any() && !MovementController.IsMoving;
        public bool CanMove => CanAct && !Stats.DisableMovementEffects.Any();
        public PlayerStats Stats { get; private set; }
        public MovementController MovementController { get; private set; }
        public Tile CurrentTile { get; set; }

        public AnimationHandler AnimationHandler { get; private set; }
        
        //TODO: Auto assign
        [field: SerializeField] public Side Side { get; private set; }
        
        private void Awake()
        {
            Stats = GetComponent<PlayerStats>();
            MovementController = GetComponent<MovementController>();
            _spellCaster = GetComponent<SpellCaster>();
            _spellBank = GetComponent<SpellBank>();
        }

        #region Initialization
        public Player WithStats(StatData statData)
        {
            Stats.SetStats(statData);
            return this;
        }

        public Player WithStatusBars(StatusBar healthBar, StatusBar manaBar)
        {
            Stats.SetStatBars(healthBar, manaBar);
            return this;
        }

        public Player WithBuffDisplay(BuffDisplay buffDisplay)
        {
            Stats.SetBuffDisplay(buffDisplay);
            return this;
        }

        public Player WithSpellDisplay(ActiveSpellDisplay spellDisplay)
        {
            spellDisplay.Init(_spellBank, _spellCaster.CooldownHandler);
            return this;
        }

        public Player WithReloadTimer(ReloadTimer reloadTimer)
        {
            reloadTimer.Init(_spellBank);
            return this;
        }

        public Player WithSpells(List<SpellData> spells)
        {
            _spellBank?.LoadSpells(spells);
            return this;
        }

        public async Task SpawnPlayer()
        {
            //spawn player here
            
            //
            AnimationHandler = new(GetComponentInChildren<Animator>());
        }
#endregion
    }
}