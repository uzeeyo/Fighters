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
            { TargetType.SingleRandom, TargetSingleRandom },
            { TargetType.MoveForward, MoveForward },
            { TargetType.MultiForward, TargetMultiForward },
            { TargetType.MultiRandomDelayed, TargetMultiRandomDelayed }
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

            TryApplyEffect(spell, targetTile);
        }

        private static void TargetSingleRandom(Player caster, Spell spell)
        {
            var targetTile = MatchManager.Grid.GetRandomTile(spell.Data.TargetSide);
            spell.transform.position = targetTile.transform.position;
            TryApplyEffect(spell, targetTile);
        }

        private static void TargetMultiForward(Player caster, Spell spell)
        {
            var targetTiles =
                MatchManager.Grid.GetTilesInDirection(caster.CurrentTile.Location, Position.Right, spell.Data.Range);
            foreach (var tile in targetTiles)
            {
                TryApplyEffect(spell, tile);
            }
        }

        private static async void MoveForward(Player caster, Spell spell)
        {
            var originalPosition = spell.transform.position;

            var timeElapsed = 0f;
            var duration = spell.Data.TravelTime;

            var maxX = originalPosition.x + MAX_TRAVEL_DISTANCE;
            var targetPosition = new Vector3(maxX, originalPosition.y, originalPosition.z);

            while (timeElapsed < duration)
            {
                if (!spell) return;
                var horizontalPercentage = spell.Data.HorizontalCurve.Evaluate(timeElapsed / duration);
                spell.transform.position =
                    originalPosition + (targetPosition - originalPosition) * horizontalPercentage;

                timeElapsed += Time.deltaTime;
                await Awaitable.NextFrameAsync();
            }

            GameObject.Destroy(spell.gameObject);
        }

        private static async void TargetMultiRandomDelayed(Player caster, Spell spell)
        {
            for (var i = 0; i < spell.Data.Range; i++)
            {
                var newSpell = GameObject.Instantiate(spell.Data.Prefab);
                newSpell.Init(spell.Data, spell.Effect);
                TargetSingleRandom(caster, newSpell);

                await Awaitable.WaitForSecondsAsync(spell.Data.RandomTimeInterval);
            }
        }

        private static async void TryApplyEffect(Spell spell, Tile tile)
        {
            await Awaitable.WaitForSecondsAsync(spell.Data.CastTime);
            float timer = 0;
            do
            {
                if (tile.Player)
                {
                    spell.Effect.Apply(tile.Player.Stats);
                    break;
                }
                timer += Time.deltaTime;
                await Awaitable.NextFrameAsync();
            } while (timer < spell.Data.Duration);
        }
    }
}