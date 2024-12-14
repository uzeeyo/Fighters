using UnityEngine;

namespace Fighters.Match
{
    public class HitEffect : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(gameObject, 2f);
        }
    }
}