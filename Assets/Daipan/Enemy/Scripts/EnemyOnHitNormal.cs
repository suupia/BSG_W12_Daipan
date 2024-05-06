#nullable enable
using UnityEngine;

namespace Enemy
{
    public class EnemyOnHitNormal : IEnemyOnHit
    {
        public void OnHit()
        {
            Debug.Log("EnemyHit Test");
        }
    }
}