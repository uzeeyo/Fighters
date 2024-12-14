using System.Collections;
using UnityEngine;

namespace Fighters.Match
{
    public class Shaker : MonoBehaviour
    {
        private static Transform s_Transform;
        private static Vector3 s_originalPosition;

        public void Awake()
        {
            s_originalPosition = transform.localPosition;
            s_Transform = transform;
        }

        public static async void Shake(float strength, float duration)
        {
            float timeElapsed = 0;
            while (timeElapsed < duration)
            {
                var posInSphere = Random.insideUnitSphere * strength;
                s_Transform.localPosition = s_originalPosition + posInSphere;
                timeElapsed += Time.deltaTime;
                await Awaitable.NextFrameAsync();
            }

            s_Transform.localPosition = s_originalPosition;
        }
    }
}