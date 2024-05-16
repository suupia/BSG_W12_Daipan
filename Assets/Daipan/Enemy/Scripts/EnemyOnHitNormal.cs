#nullable enable
using Daipan.Enemy.Interfaces;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyOnHitNormal : IEnemyOnHit
    {
        public void OnHit()
        {
            Debug.Log("EnemyHit Test");
        }
    }
}