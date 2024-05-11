using Enemy;
using UnityEngine;

namespace Enemy
{

    public class EnemyOnHit : IEnemyOnHit
    {
        public void OnHit(ENEMY_TYPE attackType)
        {
            Debug.Log(attackType);
        }
    }
}
