#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Utility.Scripts;
using Fusion;
using R3;
using UnityEngine;

namespace Daipan.Enemy.MonoScripts
{
    public sealed class EnemyNormalViewNet : AbstractEnemyViewNet
    {
        [SerializeField] HpGaugeMono hpGaugeMono = null!;
        [SerializeField] SpriteRenderer tempSpriteRenderer = null!; // todo: 完成時には削除する
        //[SerializeField] Animator animatorHighlight = null!;
        [SerializeField] NetworkMecanimAnimator animatorBody = null!;
        [SerializeField] NetworkMecanimAnimator animatorEye = null!;
        [SerializeField] NetworkMecanimAnimator animatorEyeBall = null!;
        [SerializeField] NetworkMecanimAnimator animatorLine = null!;
        [SerializeField] SpriteRenderer highlightSpriteRenderer = null!;
        [SerializeField] SpriteRenderer HpSprite=null!;
        
        IEnemyViewAnimatorSwitcher _animatorSwitcher = null!; 

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
            
            _animatorSwitcher = new EnemyViewAnimatorSwitcherNetwork(
                new [] {/*animatorHighlight,*/ animatorBody, animatorEye, animatorEyeBall, animatorLine},
                animatorLine,
                hpGaugeMono,
                highlightSpriteRenderer
            );
        }

        public override void SetDomain(IEnemyViewParamData enemyViewParamData)
        {
            animatorBody.GetComponent<SpriteRenderer>().color = enemyViewParamData.GetBodyColor();
            animatorEye.GetComponent<SpriteRenderer>().color = enemyViewParamData.GetEyeColor();
            animatorEyeBall.GetComponent<SpriteRenderer>().color = enemyViewParamData.GetEyeBallColor();
            animatorLine.GetComponent<SpriteRenderer>().color = enemyViewParamData.GetLineColor();
            highlightSpriteRenderer.color = enemyViewParamData.GetBodyColor();
            
            // temp
            tempSpriteRenderer.color = EnemyViewTempColor.GetTempColor(enemyViewParamData.GetEnemyEnum()); 
        }

        public override void SetHpGauge(double currentHp, int maxHp) => _animatorSwitcher.SetHpGauge(currentHp, maxHp);

        public override void Move() => _animatorSwitcher.Move();

        public override void Attack() => _animatorSwitcher.Attack();

        public override void Died(Action onDied) => _animatorSwitcher.Died(onDied);

        public override void Daipaned(Action onDied) => _animatorSwitcher.Daipaned(onDied);
        public override void Highlight(bool isHighlighted) => _animatorSwitcher.Highlight(isHighlighted,HpSprite);

        public void hpText()
        {
            
        }

    }
}