using System.Linq;
using UnityEngine;

namespace Match.Player
{
    public class AnimationHandler
    {
        public AnimationHandler(Animator animator)
        {
            _animator = animator;
        }
        
        private readonly Animator _animator;
        
        public async void Play(string animationName)
        {
            var hash = Animator.StringToHash(animationName);
            var clip = _animator.runtimeAnimatorController.animationClips
                .FirstOrDefault(x => x.name == animationName);
        
            if (!clip)
            {
                //Debug.LogError($"Animation clip {animationName} not found, skipping!");
                return;
            }

            float crossfadeDuration = 0.1f;
            _animator.CrossFadeInFixedTime(hash, crossfadeDuration);
            await Awaitable.WaitForSecondsAsync(clip.length + crossfadeDuration);
            
            _animator.Play("Idle");
        }
    }
}