#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Utility.Scripts;
using R3;
using UnityEngine;

namespace Daipan.Enemy.MonoScripts
{
    public class EnemyViewMono : AbstractEnemyViewMono
    {
        [SerializeField] HpGaugeMono hpGaugeMono = null!;
        [SerializeField] Animator animator = null!;

        EnemyParamDataContainer _enemyParamDataContainer = null!;
        
        void Awake()
        {
            if (hpGaugeMono == null) Debug.LogWarning("hpGaugeMono is null");
            if (animator == null) Debug.LogWarning("animator is null");
        }
        
        public override void SetDomain(EnemyParamDataContainer enemyParamDataContainer)
        {
            _enemyParamDataContainer = enemyParamDataContainer;
        }
        
        public override void SetView(NewEnemyType enemyEnum)
        {
            animator.runtimeAnimatorController = _enemyParamDataContainer.GetEnemyParamData(enemyEnum).GetAnimator();
        }
        
        public override void SetHpGauge(int currentHp, int maxHp)
        {
            hpGaugeMono.SetRatio(currentHp / (float)maxHp);
        }

        public override void Move()
        {
            animator.SetBool("IsMoving",true);
            animator.SetBool("IsAttacking", false);
        }

        public override void Attack()
        {
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsAttacking", true);
        }

        public override void Died(Action onDied)
        {
            animator.SetTrigger("OnDied");
            var preState = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
            Observable.EveryValueChanged(animator, a => a.IsEnd())
                .Where(_ => preState != animator.GetCurrentAnimatorStateInfo(0).fullPathHash) 
                .Where(isEnd => isEnd)
                .Subscribe(_ => onDied())
                .AddTo(this);
        }

        public override void Daipaned(Action onDied)
        {
            animator.SetTrigger("OnDaipaned");
            var preState = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
            Observable.EveryValueChanged(animator, a => a.IsEnd())
                .Where(_ => preState != animator.GetCurrentAnimatorStateInfo(0).fullPathHash) 
                .Where(isEnd => isEnd)
                .Subscribe(_ => onDied()) 
                .AddTo(this);
        }
        
        
        

    }
}