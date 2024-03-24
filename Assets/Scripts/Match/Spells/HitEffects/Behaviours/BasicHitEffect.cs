using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace Fighters.Match
{
    public class BasicHitEffect : MonoBehaviour, IHitEffect
    {
        private VisualEffect _visualEffect;

        private void Awake()
        {
            _visualEffect = GetComponent<VisualEffect>();
        }


        public void Play()
        {
            _visualEffect.Play();
            StartCoroutine(DestroyAfterSeconds(0.5f));

        }

        private IEnumerator DestroyAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }
    }
}