using System.Collections;
using System.Collections.Generic;
using Enemy;
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