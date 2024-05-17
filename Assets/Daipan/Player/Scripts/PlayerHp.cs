#nullable enable
using System;
using Daipan.Battle.interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Daipan.Player.Scripts
{
    public class PlayerHp : IHpSetter
    {
        readonly PlayerMono _playerMono;
        int _currentHp;
        public event EventHandler<DamageEventArgs>? OnDamage;

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
                OnDamage?.Invoke(this, new DamageEventArgs(value));
                if (_currentHp <= 0)
                {
                    Debug.Log($"Player died");
                } 
            }
        }

    }

    public record DamageEventArgs(int DamageValue);
}