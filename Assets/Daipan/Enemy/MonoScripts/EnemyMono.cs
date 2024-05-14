#nullable enable
using Daipan.Battle.interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Enemy;
using UnityEngine;
using VContainer;

namespace Daipan.Enemy.MonoScripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class EnemyMono : MonoBehaviour, IHpSetter 
    {
        public int CurrentHp
        {
            set => _enemyHp.CurrentHp = value;
            get => _enemyHp.CurrentHp;
        }
        EnemyAttack _enemyAttack = null!;
        public EnemyParameter EnemyParameter { get; private set; } = null!;

        public IEnemyOnHit EnemyOnHit { get; private set; } = null!;
        

        EnemyHp _enemyHp = null!;
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
            _enemyHp = new EnemyHp(enemyParameter.hpParameter.HPAmount, this);            

            // Sprite
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = EnemyParameter.sprite;

            var enemyOnHit = EnemyOnHit as EnemyOnHit;
            // enemyOnHit.ownEnemyType = _enemyParameter.enemyType;
        }
    }
}