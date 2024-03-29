using System;
using System.Collections;
using UnityEngine;

namespace Fighters.Utils
{
    public class Timer
    {
        public static IEnumerator CallAfterSeconds(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);
            action();
        }
    }
}