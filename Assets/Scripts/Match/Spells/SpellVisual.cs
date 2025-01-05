using System;
using UnityEngine;
using UnityEngine.VFX;

namespace Fighters.Match.Spells
{
    public class SpellVisual : MonoBehaviour
    {
        protected VisualEffect _visualEffect;

        protected virtual void Awake()
        {
            _visualEffect = GetComponent<VisualEffect>();
        }

        public void Init(Vector3 position, Quaternion rotation)
        {
            transform.rotation = rotation;
            transform.position = position;
            _visualEffect?.SendEvent("OnTarget");
        }
    }
}