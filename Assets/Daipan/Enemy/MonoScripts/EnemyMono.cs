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
        EnemyAttack _enemyAttack = null!;
        EnemyCluster _enemyCluster = null!;

        EnemyHp _enemyHp = null!;
        public EnemyParameter EnemyParameter { get; private set; } = null!;

        public IEnemyOnHit EnemyOnHit { get; private set; } = null!;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) _enemyAttack.Attack();
            if (Input.GetKeyDown(KeyCode.S)) EnemyOnHit.OnHit();

            transform.position += Vector3.left * EnemyParameter.movement.speed * Time.deltaTime;
            if (transform.position.x < -10) Destroy(gameObject); // Destroy when out of screen

            hpGaugeMono.SetRatio(CurrentHp / (float)EnemyParameter.hp.maxHp);
        }

        public int CurrentHp
        {
            set => _enemyHp.CurrentHp = value;
            get => _enemyHp.CurrentHp;
        }

        public void BlownAway()
        {
            Debug.Log("Blown away");
            _enemyCluster.RemoveEnemy(this);
            Destroy(gameObject);
            
        }


        [Inject]
        public void Initialize(
            EnemyAttack enemyAttack,
            IEnemyOnHit enemyOnHit,
            EnemyCluster enemyCluster
        )
        {
            _enemyAttack = enemyAttack;
            EnemyOnHit = enemyOnHit;
            _enemyCluster = enemyCluster;
        }

        public void SetParameter(EnemyParameter enemyParameter)
        {
            EnemyParameter = enemyParameter;
            _enemyAttack.enemyAttackParameter = EnemyParameter.attack;
            _enemyHp = new EnemyHp(enemyParameter.hp.maxHp, this, _enemyCluster);

            // Sprite
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = EnemyParameter.sprite;

            var enemyOnHit = EnemyOnHit as EnemyOnHit;
            // enemyOnHit.ownEnemyType = _enemyParameter.enemyType;
        }
    }
}