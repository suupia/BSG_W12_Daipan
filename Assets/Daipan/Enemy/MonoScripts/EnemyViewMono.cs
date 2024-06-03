#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Enemy.MonoScripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class EnemyViewMono : AbstractEnemyViewMono
    {
        [SerializeField] HpGaugeMono hpGaugeMono = null!;
        [SerializeField] SpriteRenderer spriteRenderer = null!;  // todo:まだanimatorが作りきっていないので仮置き
        [SerializeField] Animator animator = null!;

        SpriteRenderer _spriteRenderer = null!;
        Animator _animator = null!;
        
        EnemyParamDataContainer _enemyParamDataContainer = null!;
        
        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }
        
        public override void SetDomain(EnemyParamDataContainer enemyParamDataContainer)
        {
            _enemyParamDataContainer = enemyParamDataContainer;
        }
        
        public override void SetView(EnemyEnum enemyEnum)
        {
            spriteRenderer.sprite = _enemyParamDataContainer.GetEnemyParamData(enemyEnum).GetSprite();
        }
        
        public override void SetHpGauge(int currentHp, int maxHp)
        {
            hpGaugeMono.SetRatio(currentHp / (float)maxHp);
        }

        public override void Move()
        {
            _animator.SetTrigger("OnMove");
        }

        public override void Attack()
        {
            _animator.SetTrigger("OnAttack");   
        }

        public override void Died()
        {
            _animator.SetTrigger("OnDied");
        }
        
        public override void Daipaned()
        {
            _animator.SetTrigger("OnDaipaned");
        }
        
        
        

    }
}