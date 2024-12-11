#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Utility.Scripts;
using Fusion;
using R3;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.Enemy.MonoScripts
{
    public sealed class EnemyTotem2ViewNet : AbstractEnemyViewNet
    {
        [SerializeField] HpGaugeMono hpGaugeMono = null!;
        [SerializeField] SpriteRenderer tempSpriteRenderer = null!; // todo: 完成時には削除する
        //[SerializeField] Animator animatorHighlight = null!;
        [SerializeField] NetworkMecanimAnimator animatorBodyTop = null!;
        [SerializeField] NetworkMecanimAnimator animatorBodyBottom = null!;
        [SerializeField] NetworkMecanimAnimator animatorEye = null!;
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
                new [] {/*animatorHighlight, */animatorBodyTop, animatorBodyBottom, animatorEye, animatorLine},
                animatorLine,
                hpGaugeMono,
                highlightSpriteRenderer
            );
        }

        public override void SetDomain(IEnemyViewParamData enemyViewParamData)
        {
            animatorBodyTop.GetComponent<SpriteRenderer>().color = EnemyViewTempColor.GetTempColor(EnemyEnum.Red);
            animatorBodyBottom.GetComponent<SpriteRenderer>().color = EnemyViewTempColor.GetTempColor(EnemyEnum.Blue);
            animatorEye.GetComponent<SpriteRenderer>().color = enemyViewParamData.GetEyeColor();
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
        public override void Highlight(bool isHighlighted) => _animatorSwitcher.Highlight(isHighlighted, HpSprite);


    }
}