#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Enemy;
using UnityEngine;
using VContainer;

namespace Daipan.Enemy.MonoScripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class EnemyMono : MonoBehaviour
    {
        EnemyAttack _enemyAttack = null!;
        IEnemyOnHit _enemyOnHit = null!;
        EnemyParameter _enemyParameter = null!;

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

            // Sprite
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = _enemyParameter.sprite;

            var enemyOnHit = _enemyOnHit as EnemyOnHit;
            // enemyOnHit.ownEnemyType = _enemyParameter.enemyType;
        }
    }
}