using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fighters.Match.Players
{
    public class InputHandler : MonoBehaviour
    {
        private const float BUFFER_TIME = 0.25f;
        
        private Player _player;
        private SpellCaster _spellCaster;
        private Dictionary<InputCommand, Action<Vector2>> _inputActions;
        private BufferedInput _currentBuffer;
        private CancellationTokenSource _bufferCancellation;

        private enum InputCommand
        {
            Move,
            Cast
        }

        private struct BufferedInput
        {
            public InputCommand Command { get; set; }
            public Vector2 Value { get; set; }
            public float TimeStamp { get; set; }

            public bool IsExpired => 
                Time.time - TimeStamp > BUFFER_TIME;
        }

        public void Init(Player player, SpellCaster spellCaster)
        {
            _player = player;
            _spellCaster = spellCaster;
            _inputActions = new()
            {
                { InputCommand.Cast, _spellCaster.TryCast },
                { InputCommand.Move, x => _player.MovementController.TryMove(x) }
            };
        }

        private void OnDisable()
        {
            CancelBufferedInput();
        }

        private void OnMove(InputValue value)
        {
            if (!MatchManager.MatchStarted) return;
            
            var direction = value.Get<Vector2>();
            if (direction == Vector2.zero) return;

            if (_player.CanAct)
            {
                _player.MovementController.TryMove(direction);
            }
            else
            {
                BufferInput(InputCommand.Move, direction);
            }
        }

        private void OnCast(InputValue value)
        {
            if (!MatchManager.MatchStarted) return;
            
            var direction = value.Get<Vector2>();
            if (direction == Vector2.zero) return;

            if (_player.CanAct)
            {
                _spellCaster.TryCast(direction);
            }
            else
            {
                BufferInput(InputCommand.Cast, direction);
            }
        }

        private async void BufferInput(InputCommand command, Vector2 value)
        {
            CancelBufferedInput();

            _bufferCancellation = new CancellationTokenSource();
            _currentBuffer = new BufferedInput
            {
                Command = command,
                Value = value,
                TimeStamp = Time.time
            };

            try
            {
                while (!_currentBuffer.IsExpired)
                {
                    if (_bufferCancellation.Token.IsCancellationRequested)
                        return;

                    await Awaitable.NextFrameAsync();

                    if (_player.CanAct)
                    {
                        await Awaitable.NextFrameAsync();
                        ExecuteBufferedInput();
                        return;
                    }
                }
            }
            finally
            {
                CancelBufferedInput();
            }
        }

        private void ExecuteBufferedInput()
        {
            if (_currentBuffer.IsExpired) return;
            
            _inputActions[_currentBuffer.Command](_currentBuffer.Value);
        }

        private void CancelBufferedInput()
        {
            _bufferCancellation?.Cancel();
            _bufferCancellation?.Dispose();
            _bufferCancellation = null;
        }
    }
}