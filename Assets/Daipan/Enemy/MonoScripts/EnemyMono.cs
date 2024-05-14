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
        public EnemyParameter EnemyParameter { get; private set; } = null!;

        public IEnemyOnHit EnemyOnHit { get; private set; } = null!;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) _enemyAttack.Attack();
            if (Input.GetKeyDown(KeyCode.S)) EnemyOnHit.OnHit();


            transform.position += Vector3.left * Time.deltaTime;
            if (transform.position.x < -10) Destroy(gameObject);
        }


        [Inject]
        public void Initialize(EnemyAttack enemyAttack, IEnemyOnHit enemyOnHit)
        {
            _enemyAttack = enemyAttack;
            EnemyOnHit = enemyOnHit;
        }

        public void SetParameter(EnemyParameter enemyParameter)
        {
            EnemyParameter = enemyParameter;

            _enemyAttack.enemyAttackParameter = EnemyParameter.attackParameter;

            // Sprite
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = EnemyParameter.sprite;

            var enemyOnHit = EnemyOnHit as EnemyOnHit;
            // enemyOnHit.ownEnemyType = _enemyParameter.enemyType;
        }
    }
}