using Enemy;
#nullable enable
using UnityEngine;

namespace Enemy
{

    public class EnemyOnHit : IEnemyOnHit
    {
        public ENEMY_TYPE ownEnemyType;
        public void OnHit(ENEMY_TYPE attackType)
        {
            Debug.Log(attackType);
        }
    }
}
