using System;
using System.Threading;
using Fighters.Match.Spells;
using UnityEngine;

namespace Fighters.Match
{
    public class TileStateHandler
    {
        public TileStateHandler(Material material)
        {
            _material = material;
            CurrentState = TileStateFactory.GetDefault();
        }

        private readonly Material _material;
        private readonly CancellationTokenSource _stateChangeCancellation = new();
        
        public ITileState CurrentState { get; private set; }
        

        public void ChangeState(SpellData spellData)
        {
            if (spellData.TileState == TileState.Default) return;
            
            if (CurrentState is not DefaultTileState)
            {
                _stateChangeCancellation.Cancel();
            }
            
            CurrentState = TileStateFactory.Get(spellData);
            if (CurrentState is ITemporalTileState temporalState)
            {
                Countdown(temporalState);
            }
            UpdateView();
        }

        private async void Countdown(ITemporalTileState state)
        {
            try
            {
                await Awaitable.WaitForSecondsAsync(state.Duration, _stateChangeCancellation.Token);
                CurrentState = TileStateFactory.GetDefault();
                UpdateView();
            }
            catch (OperationCanceledException) { }
        }

        private void UpdateView()
        {
            foreach (var keyword in _material.enabledKeywords)
            {
                _material.DisableKeyword(keyword);
            }
            _material.EnableKeyword($"_STATE_{CurrentState.ShaderProperty.ToUpper()}");
        }
    }
}