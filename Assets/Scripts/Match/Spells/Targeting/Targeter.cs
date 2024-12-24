using System;
using System.Collections.Generic;
using Fighters.Match.Players;
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
            { TargetType.SingleMoveToTile, SingleMoveToTile },
            { TargetType.SingleRandom, TargetSingleRandom },
            { TargetType.MoveForward, MoveForward },
            { TargetType.MultiForward, TargetMultiForward },
            { TargetType.MultiMoveDelayed, MultiMoveToTile }
        };

        
        //TODO: create a strategy pattern
        public static void Target(Player caster, Spell spell)
        {
            if (spell.transform.childCount == 0)
            {
                Debug.LogError($"Spell {spell.Data.Name} has no children.");
                return;
            }

            if (_spellTargetingActions.TryGetValue(spell.Data.TargetType, out var action))
            {
                action(caster, spell);
                return;
            }

            Debug.LogError($"No action defined for {spell.Data.TargetType}");
        }

        private static void TargetSelf(Player caster, Spell spell)
        {
            var spellVisual = spell.transform.GetChild(0).GetComponent<SpellVisual>();
            spellVisual.Init(caster.transform.position, caster.transform.rotation);
            spell.Effect.Apply(caster.Stats);
        }

        private static void TargetSingle(Player caster, Spell spell)
        {
            var delta = new Position(spell.Data.Range, 0);
            if (caster.Side == Side.Opponent) delta.Inverse();
            var targetTile = MatchManager.Grid.GetTile(caster.CurrentTile.Position, delta);

            var spellVisual = spell.transform.GetChild(0).GetComponent<SpellVisual>();
            spellVisual.Init(targetTile.transform.position, caster.transform.rotation);

            TryApplyEffect(spell, targetTile);
        }

        private static void TargetSingleRandom(Player caster, Spell spell)
        {
            var targetTile = MatchManager.Grid.GetRandomTile(spell.Data.TargetSide);

            var spellVisual = spell.transform.GetChild(0).GetComponent<SpellVisual>();
            spellVisual.Init(targetTile.transform.position, caster.transform.rotation);

            TryApplyEffect(spell, targetTile);
        }


        //TODO: Not fully implemented, merge with TargetSingle
        private static void TargetMultiForward(Player caster, Spell spell)
        {
            var direction = caster.Side == Side.Self ? Position.Right : Position.Left;
            var targetTiles =
                MatchManager.Grid.GetTilesInDirection(caster.CurrentTile.Position, direction, spell.Data.Range);
            foreach (var tile in targetTiles)
            {
                TryApplyEffect(spell, tile);
            }
        }

        private static void MoveForward(Player caster, Spell spell)
        {
            var projectile = spell.transform.GetChild(0).GetComponent<Projectile>();

            var x = caster.Side == Side.Self ? MAX_TRAVEL_DISTANCE : -MAX_TRAVEL_DISTANCE;
            var maxX = spell.transform.position.x + x;
            var targetPosition = new Vector3(maxX, projectile.transform.position.y, projectile.transform.position.z);

            projectile.MoveToPosition(targetPosition);
        }

        private static void SingleMoveToTile(Player caster, Spell spell)
        {
            var delta = new Position(spell.Data.Range, 0);
            var targetTile = MatchManager.Grid.GetTile(caster.CurrentTile.Position, delta);

            spell.GetComponentInChildren<Projectile>().MoveToTile(targetTile);
        }

        private static async void MultiMoveToTile(Player caster, Spell spell)
        {
            var childCount = spell.transform.childCount;
            Tile lastTile = null;
            for (var i = childCount - 1; i >= 0; i--)
            {
                var tile = MatchManager.Grid.GetRandomTile(spell.Data.TargetSide);
                while (lastTile && tile == lastTile)
                {
                    tile = MatchManager.Grid.GetRandomTile(spell.Data.TargetSide);
                }

                spell.transform.GetChild(i).GetComponent<Projectile>().MoveToTile(tile);

                lastTile = tile;

                await Awaitable.WaitForSecondsAsync(spell.Data.RandomTimeInterval, spell.destroyCancellationToken);
            }
        }

        private static async void TryApplyEffect(Spell spell, Tile tile)
        {
            await Awaitable.WaitForSecondsAsync(spell.Data.TargetDelayAfterCast);

            float timer = 0;
            tile.ChangeState(spell.Data);
            do
            {
                if (tile.Player)
                {
                    spell.Effect.Apply(tile.Player.Stats);
                    break;
                }

                timer += Time.deltaTime;
                await Awaitable.NextFrameAsync();
            } while (timer < spell.Data.TargetDuration);
        }
    }
}