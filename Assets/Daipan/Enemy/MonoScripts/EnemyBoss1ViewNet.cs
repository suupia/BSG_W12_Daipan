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
    public sealed class EnemyBoss1ViewNet : AbstractEnemyViewNet
    {
        [SerializeField] HpGaugeMono hpGaugeMono = null!;
        [SerializeField] SpriteRenderer tempSpriteRenderer = null!; // todo: 完成時には削除する
        //[SerializeField] Animator animatorHighlight = null!;
        [SerializeField] NetworkMecanimAnimator animatorBody = null!;
        [SerializeField] NetworkMecanimAnimator animatorEye = null!;
        [SerializeField] NetworkMecanimAnimator animatorEyeBall = null!;
        [SerializeField] NetworkMecanimAnimator animatorLine = null!;
        [SerializeField] NetworkMecanimAnimator animatorTank = null!;
        [SerializeField] SpriteRenderer highlightSpriteRenderer = null!;
        [SerializeField] SpriteRenderer HpSprite=null!;

        IEnemyViewAnimatorSwitcher _animatorSwitcher = null!;
        [SerializeField] EnemyTankOffsetEventMono enemyTankOffsetEventMono = null!;
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
                new [] {/*animatorHighlight,*/ animatorBody.Animator, animatorEye.Animator, animatorEyeBall.Animator, animatorLine.Animator, animatorTank.Animator},
                animatorLine.Animator,
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
            animatorTank.GetComponent<SpriteRenderer>().color = enemyViewParamData.GetEyeColor();
            
            // temp
            tempSpriteRenderer.color = enemyViewParamData.GetEnemyEnum() switch
            {
                EnemyEnum.Red => Color.red,
                EnemyEnum.Blue => Color.blue,
                EnemyEnum.Yellow => Color.yellow,
                EnemyEnum.RedBoss => Color.Lerp(Color.red, Color.black, 0.5f), // 半分暗くする
                EnemyEnum.SpecialRed => Color.Lerp(Color.red, Color.yellow, 0.5f), // 赤と黄色の中間色
                EnemyEnum.SpecialBlue => Color.Lerp(Color.blue, Color.yellow, 0.5f), // 青と黄色の中間色
                EnemyEnum.SpecialYellow => Color.Lerp(Color.yellow, Color.red, 0.5f), // 黄色と赤の中間色
                _ => Color.white
            };
        }

        public override void SetHpGauge(double currentHp, int maxHp)
        {
            
            _animatorSwitcher.SetHpGauge(currentHp, maxHp);
            
            enemyTankOffsetEventMono.ratio = currentHp / maxHp;
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

        public override void Highlight(bool isHighlighted) => _animatorSwitcher.Highlight(isHighlighted,HpSprite);


    }
}