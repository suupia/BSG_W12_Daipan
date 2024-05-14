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
        [SerializeField] HpGaugeMono hpGaugeMono = null!;
        public int CurrentHp
        {
            set => _enemyHp.CurrentHp = value;
            get => _enemyHp.CurrentHp;
        }
        EnemyAttack _enemyAttack = null!;
        public EnemyParameter EnemyParameter { get; private set; } = null!;

        public IEnemyOnHit EnemyOnHit { get; private set; } = null!;
        
        EnemyHp _enemyHp = null!;
        EnemyCluster _enemyCluster = null!;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) _enemyAttack.Attack();
            if (Input.GetKeyDown(KeyCode.S)) EnemyOnHit.OnHit();


            transform.position += Vector3.left * Time.deltaTime;
            if (transform.position.x < -10) Destroy(gameObject);
            
            hpGaugeMono.SetRatio(CurrentHp / (float)EnemyParameter.hpParameter.HPAmount);
        }


        [Inject]
        public void Initialize(
            EnemyAttack enemyAttack, 
            IEnemyOnHit enemyOnHit,
            EnemyCluster enemyCluster)
        {
            _enemyAttack = enemyAttack;
            EnemyOnHit = enemyOnHit;
            _enemyCluster = enemyCluster;
        }

        public void SetParameter(EnemyParameter enemyParameter)
        {
            EnemyParameter = enemyParameter;
            _enemyAttack.enemyAttackParameter = EnemyParameter.attackParameter;
            _enemyHp = new EnemyHp(enemyParameter.hpParameter.HPAmount, this,_enemyCluster);            

            // Sprite
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = EnemyParameter.sprite;

            var enemyOnHit = EnemyOnHit as EnemyOnHit;
            // enemyOnHit.ownEnemyType = _enemyParameter.enemyType;
        }
    }
}