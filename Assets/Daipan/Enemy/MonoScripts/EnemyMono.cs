#nullable enable
using Daipan.Enemy.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Enemy.MonoS
{
    public class EnemyMono : MonoBehaviour
    {
        EnemyAttack _enemyAttack;
        EnemyOnHitNormal _enemyOnHitNormal;
        EnemyParameter _enemyParameter;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) _enemyAttack.Attack();
            if (Input.GetKeyDown(KeyCode.S)) _enemyOnHitNormal.OnHit();
        }

        //?????[Inject]をつけると勝手にVContainerに呼び出される？
        [Inject]
        public void Initialize(EnemyAttack enemyAttack, EnemyOnHitNormal enemyOnHitNormal)
        {
            _enemyAttack = enemyAttack;
            _enemyOnHitNormal = enemyOnHitNormal;
        }

        public void PureInitialize(EnemyParameter enemyParameter)
        {
            _enemyParameter = enemyParameter;

            _enemyAttack._enemyAttackParameter = _enemyParameter.attackParameter;
        }
    }
    
}
