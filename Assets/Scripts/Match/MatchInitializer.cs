using Fighters.Contestants;
using Fighters.Match.Players;
using Fighters.Match.Spells;
using Fighters.Match.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Fighters.Match
{
    public class MatchInitializer : MonoBehaviour
    {
        //1. Spawn players
        //2. Initialize players
        //3. Show spawn animations
        //4. Start match

        [SerializeField] private Player _playerA;
        [SerializeField] private Player _playerB;
        [SerializeField] private StatData _testStatData;
        [SerializeField] private List<SpellData> _testSpells;

        private void Start()
        {
            SetupUI();
            LoadTestData();
        }

        private void SetupUI()
        {
            var spellDisplay = FindFirstObjectByType<ActiveSpellDisplay>();
            spellDisplay.Init(_playerA.SpellBank);
            var reloadTimer = FindFirstObjectByType<ReloadTimer>();
            reloadTimer.Init(_playerA.SpellBank);
        }

        private void LoadTestData()
        {
            _playerA.Init(_testStatData, _testSpells);
            _playerB.Init(_testStatData, _testSpells);
            Destroy(gameObject);
        }
    }
}