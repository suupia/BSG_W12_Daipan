#nullable enable
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public class EnemyOnHitNormal : IEnemyOnHit
    {
        public void OnHit()
        {
            Debug.Log("EnemyHit Test");
        }
    }
}