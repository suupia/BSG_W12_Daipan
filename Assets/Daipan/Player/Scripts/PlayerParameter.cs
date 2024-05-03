#nullable enable
using System;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    [Serializable] 
    public sealed class PlayerAttackParameter 
    {
        public int AttackAmount;
    }

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerParameter", order = 1)]
    public sealed class PlayerParameter : ScriptableObject
    {
       [SerializeField] public PlayerAttackParameter attackParameter; 
    }
}