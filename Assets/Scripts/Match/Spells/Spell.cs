using Fighters.Match.Spells;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace Fighters.Match
{
    public abstract class Spell : MonoBehaviour
    {
        protected const float MAX_TRAVEL_DISTANCE = 15f;

        public string Name { get; private set; }
        public string Description { get; private set; }
        public float ManaCost { get; private set; }
        public Sprite Icon { get; private set; }
        public float Cooldown { get; private set; }
        public float CastTime { get; private set; }
        public VisualEffect Vfx { get; protected set; }

        protected virtual void Awake()
        {
            StartCoroutine(DelayDestroy());
        }

        public virtual void Init(SpellData data)
        {
            Name = data.Name;
            Description = data.Description;
            ManaCost = data.ManaCost;
            Icon = data.Icon;
            Cooldown = data.Cooldown;
            CastTime = data.CastTime;
            Vfx = GetComponent<VisualEffect>();
        }

        public abstract IEnumerator Cast(Tile origin);

        private IEnumerator DelayDestroy()
        {
            yield return new WaitForSeconds(2.5f);
            Destroy(gameObject);
        }
    }
}