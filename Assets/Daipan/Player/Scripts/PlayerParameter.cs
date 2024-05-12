#nullable enable
using System;
using UnityEngine;

namespace Stream.Player.Scripts
{
    [Serializable]
    public sealed class PlayerAttackParameter
    {
        public int AttackAmount;
        public int SAttackAmount;
    }
    

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerParameter", order = 1)]
    public sealed class PlayerParameter : ScriptableObject
    {
        [SerializeField] public PlayerAttackParameter attackParameter = null!;
    }
}