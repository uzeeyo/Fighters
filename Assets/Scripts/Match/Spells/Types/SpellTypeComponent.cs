using UnityEngine;

namespace Fighters.Match.Spells
{
    public abstract class SpellTypeComponent : MonoBehaviour
    {
        public abstract void SetData(SpellData data);
    }
}