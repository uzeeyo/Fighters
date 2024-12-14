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
            State = TileStateFactory.GetDefault();
        }

        private readonly Material _material;
        private readonly CancellationTokenSource _stateChangeCancellation = new();
        private string _previousState = "Default";
        
        public ITileState State { get; private set; }
        

        public void ChangeState(SpellData spellData)
        {
            Debug.Log("Changing tile state to " + State.ShaderProperty);
            if (State is not DefaultTileState)
            {
                _stateChangeCancellation.Cancel();
            }
            State = TileStateFactory.Get(spellData);
            if (State is ITemporalTileState temporalState)
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
                State = TileStateFactory.GetDefault();
                UpdateView();
            }
            catch (OperationCanceledException) {}
        }

        private void UpdateView()
        {
            foreach (var keyword in _material.enabledKeywords)
            {
                _material.DisableKeyword(keyword);
            }
            _material.EnableKeyword($"_STATE_{State.ShaderProperty.ToUpper()}");
            _previousState = State.ShaderProperty;
        }
    }
}