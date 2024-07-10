#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Utility.Scripts;
using R3;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.Enemy.MonoScripts
{
    public sealed class EnemyBoss3ViewMono : AbstractEnemyViewMono
    {
        [SerializeField] HpGaugeMono hpGaugeMono = null!;
        [SerializeField] SpriteRenderer tempSpriteRenderer = null!; // todo: 完成時には削除する
        [SerializeField] Animator animatorHighlight = null!;
        [SerializeField] Animator animatorBody = null!;
        [SerializeField] Animator animatorEye = null!;
        [SerializeField] Animator animatorFoot = null!;
        [SerializeField] Animator animatorLine = null!;
        [SerializeField] SpriteRenderer highlightSpriteRenderer = null!;

        EnemyViewAnimatorSwitcher _animatorSwitcher = null!;
        void Awake()    
        {
            if (hpGaugeMono == null)
            {
                Debug.LogWarning("hpGaugeMono is null");
                return;
            }

            if (tempSpriteRenderer == null)
            {
                Debug.LogWarning("tempSpriteRenderer is null");
                return;
            }
            
            _animatorSwitcher = new EnemyViewAnimatorSwitcher(
                new [] {animatorHighlight, animatorBody, animatorEye, animatorFoot, animatorLine, },
                animatorLine,
                hpGaugeMono,
                highlightSpriteRenderer
            );
        }


        public override void SetDomain(IEnemyViewParamData enemyViewParamData)
        {
            animatorBody.GetComponent<SpriteRenderer>().color = enemyViewParamData.GetBodyColor();
            animatorEye.GetComponent<SpriteRenderer>().color = enemyViewParamData.GetEyeColor();
            animatorFoot.GetComponent<SpriteRenderer>().color = enemyViewParamData.GetEyeColor();
            animatorLine.GetComponent<SpriteRenderer>().color = enemyViewParamData.GetLineColor();
            
            // temp
            tempSpriteRenderer.color = EnemyViewTempColor.GetTempColor(enemyViewParamData.GetEnemyEnum()); 

        }

        public override void SetHpGauge(double currentHp, int maxHp)
        {
            _animatorSwitcher.SetHpGauge(currentHp, maxHp);
            
        }

        public override void Move()
        {
            _animatorSwitcher.Move();
        }

        public override void Attack()
        {
            _animatorSwitcher.Attack();
        }

        public override void Died(Action onDied)
        {
            _animatorSwitcher.Died(onDied);
        }

        public override void Daipaned(Action onDied)
        {
            _animatorSwitcher.Daipaned(onDied);
        }

        public override void Highlight(bool isHighlighted) => _animatorSwitcher.Highlight(isHighlighted);


    }
}