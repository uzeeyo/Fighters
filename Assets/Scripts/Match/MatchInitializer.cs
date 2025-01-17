using Fighters.Contestants;
using Fighters.Match.Players;
using Fighters.Match.Spells;
using Fighters.Match.UI;
using System.Collections.Generic;
using Fighters.UI;
using Match.UI;
using UnityEngine;

namespace Fighters.Match
{
    public class MatchInitializer : MonoBehaviour
    {
        [Header("Test stuff")]
        [SerializeField] private Player _playerA;
        [SerializeField] private Player _playerB;
        [SerializeField] private StatData _testStatData;
        [SerializeField] private List<SpellData> _testSpells;
        [SerializeField] private List<SpellData> _aiSpells;
        
        [Header("UI Elements")]
        [SerializeField] private StatusBar _selfHealthBar;
        [SerializeField] private StatusBar _selfManaBar;
        [SerializeField] private BuffDisplay _selfBuffDisplay;
        [SerializeField] private ActiveSpellDisplay _activeSpellDisplay;
        [SerializeField] private ReloadTimer _reloadTimer;
        
        [Header("Enemy UI Elements")]
        [SerializeField] private StatusBar _enemyHealthBar;
        [SerializeField] private StatusBar _enemyManaBar;
        [SerializeField] private BuffDisplay _enemyBuffDisplay;
        
        //1. Spawn players
        //2. Initialize players
        //3. Show spawn animations
        //4. Start match

        private void Start()
        {
            Setup();
        }
        
        private async void Setup()
        {
            await _playerA.Init()
                .WithStatusBars(_selfHealthBar, _selfManaBar)
                .WithSpellDisplay(_activeSpellDisplay)
                .WithBuffDisplay(_selfBuffDisplay)
                .WithReloadTimer(_reloadTimer)
                .WithSpells(_testSpells)
                .WithStats(_testStatData)
                .WithSide(Side.Self)
                .SpawnPlayer();

            await _playerB.Init()
                .WithStatusBars(_enemyHealthBar, _enemyManaBar)
                .WithBuffDisplay(_enemyBuffDisplay)
                .WithSpells(_aiSpells)
                .WithStats(_testStatData)
                .WithSide(Side.Opponent)
                .SpawnPlayer();

            var matchManager = FindFirstObjectByType<MatchManager>();
            
            matchManager.StartMatch();
            Destroy(gameObject);
        }
    }
}