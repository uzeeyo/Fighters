using System.Collections.Generic;
using Fighters.Buffs;
using Match.Player;
using UnityEngine;

namespace Match.UI
{
    public class BuffDisplay : MonoBehaviour
    {
        private BuffHandler _buffHandler;
        private List<BuffIcon> _buffIcons;
        
        [SerializeField] private BuffIcon _buffIconPrefab;

        public void Init(BuffHandler buffHandler)
        {
            _buffHandler = buffHandler;
            _buffHandler.BuffAdded += OnBuffAdded;
            _buffHandler.BuffRemoved += OnBuffRemoved;
        }


        private void OnBuffAdded(Buff buff)
        {
            var icon  = Instantiate(_buffIconPrefab, transform);
            _buffIcons.Add(icon);
            icon.Init(buff.Icon, buff.Duration);
        }

        private void OnBuffRemoved(Buff buff)
        {
            
        }
        
        private void OnDestroy()
        {
            _buffHandler.BuffAdded -= OnBuffAdded;
            _buffHandler.BuffRemoved -= OnBuffRemoved;
        }
    }
}