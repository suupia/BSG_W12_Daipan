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
        [SerializeField] Animator animatorBody = null!;
        [SerializeField] Animator animatorEye = null!;
        [SerializeField] Animator animatorEyeBall = null!;
        [SerializeField] Animator animatorLine = null!;

        EnemyParamDataContainer _enemyParamDataContainer = null!;
        
        void Awake()
        {
            if (hpGaugeMono == null) Debug.LogWarning("hpGaugeMono is null");
        }
        
        public override void SetDomain(EnemyParamDataContainer enemyParamDataContainer)
        {
            _enemyParamDataContainer = enemyParamDataContainer;
        }
        
        public override void SetView(EnemyEnum enemyEnum)
        {
            // animator.runtimeAnimatorController = _enemyParamDataContainer.GetEnemyParamData(enemyEnum).GetAnimator();
            animatorBody.GetComponent<SpriteRenderer>().color = Color.blue;
            animatorEye.GetComponent<SpriteRenderer>().color = Color.blue;
            animatorEyeBall.GetComponent<SpriteRenderer>().color = Color.blue;
            animatorLine.GetComponent<SpriteRenderer>().color = Color.blue;
            
        }
        
        public override void SetHpGauge(int currentHp, int maxHp)
        {
            hpGaugeMono.SetRatio(currentHp / (float)maxHp);
        }

        public override void Move()
        {
            SetBoolAll("IsMoving", true);
            SetBoolAll("IsAttacking", false);
        }

        public override void Attack()
        {
            SetBoolAll("IsMoving", false);
            SetBoolAll("IsAttacking", true);
        }

        public override void Died(Action onDied)
        {
            // animatorLineを代表とする
            animatorLine.SetTrigger("OnDied");
            var preState = animatorLine.GetCurrentAnimatorStateInfo(0).fullPathHash;
            Observable.EveryValueChanged(animatorLine, a => a.IsEnd())
                .Where(_ => preState != animatorLine.GetCurrentAnimatorStateInfo(0).fullPathHash) 
                .Where(isEnd => isEnd)
                .Subscribe(_ => onDied())
                .AddTo(this);
        }

        public override void Daipaned(Action onDied)
        {
            // animatorLineを代表とする
            animatorLine.SetTrigger("OnDaipaned");
            var preState = animatorLine.GetCurrentAnimatorStateInfo(0).fullPathHash;
            Observable.EveryValueChanged(animatorLine, a => a.IsEnd())
                .Where(_ => preState != animatorLine.GetCurrentAnimatorStateInfo(0).fullPathHash) 
                .Where(isEnd => isEnd)
                .Subscribe(_ => onDied()) 
                .AddTo(this);
        }

        void SetTriggerAll(string paramName)
        {
            animatorBody.SetTrigger(paramName);
            animatorEye.SetTrigger(paramName);
            animatorEyeBall.SetTrigger(paramName);
            animatorLine.SetTrigger(paramName);
        }
        
        void SetBoolAll(string paramName, bool value)
        {
            animatorBody.SetBool(paramName, value);
            animatorEye.SetBool(paramName, value);
            animatorEyeBall.SetBool(paramName, value);
            animatorLine.SetBool(paramName, value);
        }
        

    }
}