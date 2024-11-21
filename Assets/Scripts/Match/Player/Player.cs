using Fighters.Contestants;
using Fighters.Match.Spells;
using System.Collections.Generic;
using System.Linq;
using Match.Player;
using UnityEngine;

namespace Fighters.Match.Players
{
    [RequireComponent(typeof(PlayerStats), typeof(SpellBank))]
    public class Player : MonoBehaviour
    {
        private Animator _animator;


        public bool CanInteract { get; set; } = true;
        public PlayerStats Stats { get; private set; }
        public SpellBank SpellBank { get; private set; }
        public MovementController MovementController { get; private set; }
        public Tile CurrentTile { get; set; }
        //TODO: Auto assign 
        [field: SerializeField] public Side Side { get; private set; }
        public BuffHandler BuffHandler { get; private set; } = new BuffHandler();


        private void Awake()
        {
            Stats = GetComponent<PlayerStats>();
            SpellBank = GetComponent<SpellBank>();
            MovementController = GetComponent<MovementController>();
            _animator = GetComponentInChildren<Animator>();
        }

        public void Init(StatData statData, List<SpellData> spells)
        {
            Stats.Init(statData);
            SpellBank?.LoadSpells(spells);
        }

        public async void PlayAnimation(string animationName)
        {
            var hash = Animator.StringToHash(animationName);
            var clip = _animator.runtimeAnimatorController.animationClips
                .FirstOrDefault(x => x.name == animationName);
        
            if (!clip)
            {
                Debug.LogError($"Animation clip {animationName} not found, skipping!");
                return;
            }

            float crossfadeDuration = 0.1f;
            _animator.CrossFadeInFixedTime(hash, crossfadeDuration);
            await Awaitable.WaitForSecondsAsync(clip.length + crossfadeDuration);
            
            _animator.Play("Idle");
        }
    }
}