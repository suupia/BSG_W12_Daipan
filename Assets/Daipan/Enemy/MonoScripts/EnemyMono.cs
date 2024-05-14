#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Enemy;
using UnityEngine;
using VContainer;

namespace Daipan.Enemy.MonoScripts
{
    public class EnemyMono : MonoBehaviour
    {
        EnemyAttack _enemyAttack;
        IEnemyOnHit _enemyOnHit;
        EnemyParameter _enemyParameter;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) _enemyAttack.Attack();
            if (Input.GetKeyDown(KeyCode.S)) _enemyOnHit.OnHit();


            transform.position += Vector3.left * Time.deltaTime;
            if (transform.position.x < -10) Destroy(gameObject);
        }


        [Inject]
        public void Initialize(EnemyAttack enemyAttack, IEnemyOnHit enemyOnHit)
        {
            _enemyAttack = enemyAttack;
            _enemyOnHit = enemyOnHit;
        }

        public void SetParameter(EnemyParameter enemyParameter)
        {
            _enemyParameter = enemyParameter;

            _enemyAttack.enemyAttackParameter = _enemyParameter.attackParameter;

            var enemyOnHit = _enemyOnHit as EnemyOnHit;
            // enemyOnHit.ownEnemyType = _enemyParameter.enemyType;
        }
    }
}