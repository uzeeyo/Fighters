using System.Collections.Generic;
using System.Linq;
using Fighters.Buffs;
using Fighters.Match.Spells;
using Match.Player;
using UnityEngine;

namespace Match.UI
{
    public class BuffDisplay : MonoBehaviour
    {
        private BuffHandler _buffHandler;
        private readonly List<BuffIcon> _buffIcons = new();
        
        [SerializeField] private BuffIcon _buffIconPrefab;

        public void Init(BuffHandler buffHandler)
        {
            _buffHandler = buffHandler;
            _buffHandler.BuffAdded += OnBuffAdded;
            _buffHandler.BuffRemoved += OnBuffRemoved;
        }


        private void OnBuffAdded(BuffEffect buff)
        {
            var icon  = Instantiate(_buffIconPrefab, transform);
            _buffIcons.Add(icon);
            icon.Init(buff);
        }

        private void OnBuffRemoved(BuffEffect buff)
        {
            try
            {
                var icon = _buffIcons.First(x => x.Effect == buff);
                _buffIcons.Remove(icon);
                Destroy(icon.gameObject);
            }
            catch
            {
                Debug.LogError($"Failed to remove buff {buff.BuffType} from display");
            }
        }
        
        private void OnDestroy()
        {
            _buffHandler.BuffAdded -= OnBuffAdded;
            _buffHandler.BuffRemoved -= OnBuffRemoved;
        }
    }
}