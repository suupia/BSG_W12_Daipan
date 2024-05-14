#nullable enable
using System;
using UnityEngine;

namespace Daipan.Stream.Scripts
{
    [Serializable]
    public sealed class PlayerAttackParameter
    {
        public int AttackAmount;
        public int SAttackAmount = 10;
        public int WAttackAmount = 20;
        public int AAttackAmount = 30;
    }
    

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerParameter", order = 1)]
    public sealed class PlayerParameter : ScriptableObject
    {
        [SerializeField] public PlayerAttackParameter attackParameter = null!;
    }
}