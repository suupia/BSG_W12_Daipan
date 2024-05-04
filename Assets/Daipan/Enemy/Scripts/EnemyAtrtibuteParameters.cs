#nullable enable
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyAttributeParameters", order = 1)]
    public sealed class EnemyAttributeParameters : ScriptableObject
    {
        public List<EnemyParameter> enemyParameters = new ();
    }
}