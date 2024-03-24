using UnityEngine;
using UnityEngine.UI;

namespace Fighters.Match.Spells
{
    public class SpellIcon : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        public Image Icon => _icon;
    }
}