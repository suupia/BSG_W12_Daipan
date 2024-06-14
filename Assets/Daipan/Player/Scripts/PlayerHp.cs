#nullable enable
using System;
using Daipan.Battle.interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Daipan.Player.Scripts
{
    public class PlayerHp : IPlayerHp 
    {
        public int MaxHp { get; }
        public event EventHandler<DamageArgs>? OnDamage;

        public PlayerHp(int maxHp)
        {
            MaxHp = maxHp;
            CurrentHp = maxHp;
        }

        public int CurrentHp { get; set; }

        public void SetHp(DamageArgs damageArgs)
        {
            CurrentHp -= damageArgs.DamageValue;
            OnDamage?.Invoke(this, damageArgs);
            if (CurrentHp <= 0)
            {
                Debug.Log($"Player died");
                // todo: コールバックで次のシーンへの遷移を挟みたい
            }
        }

    }

    
}