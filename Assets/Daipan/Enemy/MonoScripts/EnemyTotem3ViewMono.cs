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
    public sealed class EnemyTotem3ViewMono : AbstractEnemyViewMono
    {
        [SerializeField] HpGaugeMono hpGaugeMono = null!;
        [SerializeField] SpriteRenderer tempSpriteRenderer = null!; // todo: 完成時には削除する
        [SerializeField] Animator animatorHighlight = null!;
        [SerializeField] Animator animatorBodyTop = null!;
        [SerializeField] Animator animatorMiddle = null!;
        [SerializeField] Animator animatorBodyBottom = null!;
        [SerializeField] Animator animatorEye = null!;
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
                new [] {animatorHighlight, animatorBodyTop, animatorMiddle, animatorBodyBottom, animatorEye, animatorLine},
                animatorLine,
                hpGaugeMono,
                highlightSpriteRenderer
            );
        }

        public override void SetDomain(IEnemyViewParamData enemyViewParamData)
        {
            animatorBodyTop.GetComponent<SpriteRenderer>().color = EnemyViewTempColor.GetTempColor(EnemyEnum.Red);
            animatorMiddle.GetComponent<SpriteRenderer>().color = EnemyViewTempColor.GetTempColor(EnemyEnum.Blue);
            animatorBodyBottom.GetComponent<SpriteRenderer>().color = EnemyViewTempColor.GetTempColor(EnemyEnum.Yellow);
            animatorEye.GetComponent<SpriteRenderer>().color = enemyViewParamData.GetEyeColor();
            animatorLine.GetComponent<SpriteRenderer>().color = enemyViewParamData.GetLineColor();
            
            // temp
            tempSpriteRenderer.color = EnemyViewTempColor.GetTempColor(enemyViewParamData.GetEnemyEnum()); 

        }

        public override void SetHpGauge(double currentHp, int maxHp) => _animatorSwitcher.SetHpGauge(currentHp, maxHp);

        public override void Move() => _animatorSwitcher.Move();

        public override void Attack() => _animatorSwitcher.Attack();

        public override void Died(Action onDied) => _animatorSwitcher.Died(onDied);

        public override void Daipaned(Action onDied) => _animatorSwitcher.Daipaned(onDied);
        public override void Highlight(bool isHighlighted) => _animatorSwitcher.Highlight(isHighlighted);


    }
}