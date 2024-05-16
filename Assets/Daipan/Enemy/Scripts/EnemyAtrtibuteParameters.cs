#nullable enable
using System.Collections.Generic;
using UnityEngine;


namespace Daipan.Enemy.Scripts
{
    
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy/AttributeParameters", order = 1)]
    public sealed class EnemyAttributeParameters : ScriptableObject
    {
        public List<EnemyParameter> enemyParameters = new ();
    }
}