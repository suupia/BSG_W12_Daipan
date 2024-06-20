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
        [SerializeField] SpriteRenderer tempSpriteRenderer = null!; // todo: 完成時には削除する
        [SerializeField] Animator animatorBody = null!;
        [SerializeField] Animator animatorEye = null!;
        [SerializeField] Animator animatorEyeBall = null!;
        [SerializeField] Animator animatorLine = null!;

        EnemyParamDataContainer _enemyParamDataContainer = null!;

        void Awake()
        {
            if (hpGaugeMono == null) Debug.LogWarning("hpGaugeMono is null");
            if (tempSpriteRenderer == null) Debug.LogWarning("tempSpriteRenderer is null");

        }

        public override void SetDomain(EnemyParamDataContainer enemyParamDataContainer)
        {
            _enemyParamDataContainer = enemyParamDataContainer;
        }

        public override void SetView(EnemyEnum enemyEnum)
        {
            // temp
            tempSpriteRenderer.color = enemyEnum switch
            {
                EnemyEnum.Red => Color.red,
                EnemyEnum.Blue => Color.blue,
                EnemyEnum.Yellow => Color.yellow,
                EnemyEnum.RedBoss => Color.Lerp(Color.red, Color.black, 0.5f), // 半分暗くする
                EnemyEnum.Special => Color.Lerp(Color.red, Color.yellow, 0.5f), // 赤と黄色の中間色
                _ => Color.white
            };
            var enemyParamData = _enemyParamDataContainer.GetEnemyParamData(enemyEnum);
            animatorBody.GetComponent<SpriteRenderer>().color = enemyParamData.GetBodyColor();
            animatorEye.GetComponent<SpriteRenderer>().color = enemyParamData.GetEyeColor();
            animatorEyeBall.GetComponent<SpriteRenderer>().color = enemyParamData.GetEyeBallColor();
            animatorLine.GetComponent<SpriteRenderer>().color = enemyParamData.GetLineColor();

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
            SetTriggerAll("OnDied");
            // animatorLineを代表とする
            var preState = animatorLine.GetCurrentAnimatorStateInfo(0).fullPathHash;
            Observable.EveryValueChanged(animatorLine, a => a.IsEnd())
                .Where(_ => preState != animatorLine.GetCurrentAnimatorStateInfo(0).fullPathHash) 
                .Where(isEnd => isEnd)
                .Subscribe(_ => onDied())
                .AddTo(this);
        }

        public override void Daipaned(Action onDied)
        {
            SetTriggerAll("OnDaipaned");
            // animatorLineを代表とする
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