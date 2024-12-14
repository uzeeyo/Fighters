using System.Linq;
using UnityEngine;

namespace Match.Player
{
    public class AnimationHandler
    {
        const float CROSSFADE_DURATION = 0.1f;
        public AnimationHandler(Animator animator)
        {
            _animator = animator;
        }
        
        private readonly Animator _animator;
        
        public float Play(string animationName)
        {
            var hash = Animator.StringToHash(animationName);
            var clip = _animator.runtimeAnimatorController.animationClips
                .FirstOrDefault(x => x.name == animationName);
        
            if (!clip)
            {
                Debug.LogWarning($"Animation clip {animationName} not found, skipping!");
                return 0;
            }
            
            _animator.CrossFadeInFixedTime(hash, CROSSFADE_DURATION);
            
            
            ReturnToIdle(clip.length);
            return clip.length;
            
        }

        private async void ReturnToIdle(float duration)
        {
            await Awaitable.WaitForSecondsAsync(duration);
            
            _animator.Play("Idle");
        }
    }
}