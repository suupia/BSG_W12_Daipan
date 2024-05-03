#nullable enable
using System;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    [Serializable] 
    public class PlayerAttackParameter 
    {
        public int AttackAmount;
    }

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerAttackParameter", order = 1)]
    public class PlayerParameter : ScriptableObject
    {
       [SerializeField] public PlayerAttackParameter attackParameter; 
    }
}