using System;
using System.Collections.Generic;
using Fighters.Match.Players;
using Unity.VisualScripting;
using UnityEngine;

namespace Fighters.Match.Spells
{
    public static class Targeter
    {
        private const float MAX_TRAVEL_DISTANCE = 15f;

        private static Dictionary<TargetType, Action<Player, Spell>> _spellTargetingActions = new()
        {
            { TargetType.Self, TargetSelf },
            { TargetType.Single, TargetSingle },
            { TargetType.MoveForward, MoveForward },
            { TargetType.MultiForward, TargetMultiForward },
        };

        public static void Target(Player caster, Spell spell)
        {
            if (_spellTargetingActions.TryGetValue(spell.Data.TargetType, out var action))
            {
                action(caster, spell);
                return;
            }

            Debug.LogError($"No action defined for {spell.Data.TargetType}");
        }

        private static void TargetSelf(Player caster, Spell spell)
        {
            spell.transform.position = caster.transform.position;
            spell.Effect.Apply(caster.Stats);
        }

        private static void TargetSingle(Player caster, Spell spell)
        {
            var delta = new Position(spell.Data.Range, 0);
            var targetTile = MatchManager.Grid.GetTile(caster.CurrentTile.Location, delta);
            spell.transform.position = targetTile.transform.position;

            ApplyEffect(spell, targetTile);
        }

        private static void TargetMultiForward(Player caster, Spell spell)
        {
            var targetTiles =
                MatchManager.Grid.GetTilesInDirection(caster.CurrentTile.Location, Position.Right, spell.Data.Range);
            foreach (var tile in targetTiles)
            {
                ApplyEffect(spell, tile);
            }
        }

        private static async void MoveForward(Player caster, Spell spell)
        {
            var originalPosition = caster.transform.position;
            spell.transform.position = originalPosition;

            var timeElapsed = 0f;
            var duration = spell.Data.TravelTime;

            var maxX = originalPosition.x + MAX_TRAVEL_DISTANCE;
            var targetPosition = new Vector3(maxX, originalPosition.y, originalPosition.z);

            while (timeElapsed < duration)
            {
                var horizontalPercentage = spell.Data.HorizontalCurve.Evaluate(timeElapsed / duration);
                spell.transform.position =
                    originalPosition + (targetPosition - originalPosition) * horizontalPercentage;

                timeElapsed += Time.deltaTime;
                await Awaitable.NextFrameAsync();
            }

            GameObject.Destroy(spell.gameObject);
        }

        private static async void ApplyEffect(Spell spell, Tile tile)
        {
            await Awaitable.WaitForSecondsAsync(spell.Data.CastTime);
            if (tile.Player)
            {
                spell.Effect.Apply(tile.Player.Stats);
            }
        }
    }
}