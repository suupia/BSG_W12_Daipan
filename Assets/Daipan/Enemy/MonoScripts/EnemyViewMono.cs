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
        [SerializeField] SpriteRenderer spriteRenderer = null!;  // todo:まだanimatorが作りきっていないので仮置き
        [SerializeField] Animator animator = null!;

        
        EnemyParamDataContainer _enemyParamDataContainer = null!;
        
        void Awake()
        {
            if (hpGaugeMono == null) Debug.LogWarning("hpGaugeMono is null");
            if (spriteRenderer == null) Debug.LogWarning("spriteRenderer is null");
            if (animator == null) Debug.LogWarning("animator is null");
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
            Debug.Log($"Died GetAnimatorTransitionInfo.IsName(hoge): {animator.GetAnimatorTransitionInfo(0).IsName("hoge")}");
            Debug.Log("Died Current State before trigger: " + animator.GetCurrentAnimatorStateInfo(0).fullPathHash);
            Debug.Log($"Died Next State before trigger: {animator.GetNextAnimatorStateInfo(0).fullPathHash}");
            animator.SetTrigger("OnDied");
            Debug.Log("Died animation");
            Debug.Log($"IsDied: {animator.IsEnd()}");
            Debug.Log($"Died normalized time: {animator.GetCurrentAnimatorStateInfo(0).normalizedTime}");
            Debug.Log($"Died Current State: {animator.GetCurrentAnimatorStateInfo(0).fullPathHash}");
            Debug.Log($"Died Next State: {animator.GetNextAnimatorStateInfo(0).fullPathHash}");
            Observable.EveryValueChanged(animator, a => a.IsEnd())
                .Where(isEnd => isEnd && !animator.IsInTransition(0))
                .Subscribe(_ => onDied())
                .AddTo(this);
        }

        void Update()
        {
            Debug.Log($"Died Update state name: {animator.GetCurrentAnimatorStateInfo(0).fullPathHash}");
            Debug.Log($"animation normalized time: {animator.GetCurrentAnimatorStateInfo(0).normalizedTime}");
            
            if(Input.GetKeyDown(KeyCode.I)) 
            {
                Died(() => Debug.Log("Died"));
            }
        }

        public override void Daipaned()
        {
            animator.SetTrigger("OnDaipaned");
        }
        
        
        

    }
}