using System;
using System.Collections;
using UnityEngine;

namespace Fighters.Utils
{
    public class Timer
    {
        public static IEnumerator CallAfterSeconds(float seconds, Action action)
        {
            var timeElapsed = 0f;
            var duration = seconds;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            action();
        }
    }
}