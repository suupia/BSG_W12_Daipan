#nullable enable
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.Player.Scripts
{
    [Serializable]
    public sealed class PlayerAttackParameter
    {
        public int AttackAmount;
        public int SAttackAmount = 10;
        public int WAttackAmount = 20;
        public int AAttackAmount = 30;
    }

    [Serializable]
    public sealed class PlayerHpParameter
    {
        public int maxHp;
    }


    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerParameter", order = 1)]
    public sealed class PlayerParameter : ScriptableObject
    {
        [SerializeField] public PlayerAttackParameter attack = null!;
        [SerializeField] public PlayerHpParameter hp = null!;
    }
}