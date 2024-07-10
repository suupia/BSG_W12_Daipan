#nullable enable
using System;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public record Hp
    {
        public int Value { get; private set; }

        public Hp(int maxHp)
        {
            Value = maxHp;
        }

        public void Increase(int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Amount must be non-negative.");
                return;
            }

            Value += amount;
        }

        public void Decrease(int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Amount must be non-negative.");
                return;
            }

            Value = Math.Max(Value - amount, 0);
        }
    }
}