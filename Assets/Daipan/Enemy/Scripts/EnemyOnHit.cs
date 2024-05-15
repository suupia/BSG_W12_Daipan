using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Enemy;
#nullable enable
using UnityEngine;

namespace Enemy
{
    public class EnemyOnHit : IEnemyOnHit
    {
        public void OnHit()
        {
            Debug.Log("EnemyHit Test");
        }
    }
}
