#nullable enable
using Daipan.Battle.interfaces;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public class PlayerHp : IHpSetter
    {
        readonly PlayerMono _playerMono;
        int _currentHp;

        public PlayerHp(int maxHp, PlayerMono playerMono)
        {
            CurrentHp = maxHp;
            _playerMono = playerMono;
        }

        public int CurrentHp
        {
            get => _currentHp;
            set
            {
                _currentHp = value;
                Debug.Log($"Player CurrentHp : {_currentHp}");
                if (_currentHp <= 0)
                {
                    Debug.Log($"Player died");
                } 
            }
        }

    }
}