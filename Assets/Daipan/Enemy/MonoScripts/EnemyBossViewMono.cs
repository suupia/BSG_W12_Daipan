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
    public class EnemyBossViewMono : AbstractEnemyViewMono
    {
        [SerializeField] HpGaugeMono hpGaugeMono = null!;
        [SerializeField] SpriteRenderer tempSpriteRenderer = null!; // todo: 完成時には削除する
        [SerializeField] Animator animatorHighlight = null!;
        [SerializeField] Animator animatorBody = null!;
        [SerializeField] Animator animatorEye = null!;
        [SerializeField] Animator animatorEyeBall = null!;
        [SerializeField] Animator animatorLine = null!;
        [SerializeField] Animator animatorTank = null!;
        [SerializeField] SpriteRenderer highlightSpriteRenderer = null!;

        EnemyViewAnimatorSwitcher _animatorSwitcher = null!;
        Material? _tankGaugeMaterial;
        [SerializeField] double[] offsetRatio ={ 0, 0, 0.3, 0.3, 0.05, 0, 0, 0 };
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
            
            _animatorSwitcher = new EnemyViewAnimatorSwitcher(
                new [] {animatorHighlight, animatorBody, animatorEye, animatorEyeBall, animatorLine, animatorTank},
                animatorLine,
                hpGaugeMono,
                highlightSpriteRenderer
            );
            _tankGaugeMaterial = animatorTank.GetComponent<SpriteRenderer>().material;
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
                EnemyEnum.Special => Color.Lerp(Color.red, Color.yellow, 0.5f), // 赤と黄色の中間色
                _ => Color.white
            };
        }

        public override void SetHpGauge(double currentHp, int maxHp)
        {
            
            _animatorSwitcher.SetHpGauge(currentHp, maxHp);
            
            if (_tankGaugeMaterial == null)
            {
                Debug.LogWarning("_tankGaugeMaterial is null");
                return;
            }

            enemyTankOffsetEventMono.Ratio = currentHp / maxHp;
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

        void TankSpriteOffset()
        {
            // 全部で画像は6枚なのでそれに対応するように作る
            var animationTime = new[] { 0.00, 0.01, 0.02, 0.04, 0.05, 0.07 };
            var offsetRatio = new[] { 0, 0, 0.1, 0.05, 0, 0 };
        }

    }

    // Tankのアニメーションが上下するので、それに合わせて画像を切る位置を変更する
    class TankSpriteFitter
    {
        // 全部で画像は6枚なのでそれに対応するように作る
         double[] _offsetRatio = { 0, 0, 0.3, 0.3, 0.05, 0, 0, 0 };

        double Timer { get; set; }
        int CurrentIndex { get; set; }
        double CurrentOffsetRatio => _offsetRatio[CurrentIndex];
       
        // Tankの画像が全体の画像のサイズに合わせられているため、0.47でマックスになることに注意
        const double FillMax = 0.6;

        double Ratio { get; set; }
        public void Update(Animator animator, Material gaugeMaterial,double[] offsetRatio, out int animationIndex)
        {
            _offsetRatio = offsetRatio; // デバッグ
            
            var normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            
            // アニメーションの長さを8分割する
            animationIndex = Mathf.FloorToInt(normalizedTime * 8) % 8;
            Debug.Log($"animationIndex: {animationIndex}");
            
            gaugeMaterial.SetFloat("_Ratio", (float)((Ratio + _offsetRatio[animationIndex]) * FillMax));
        }
        
   

        public void SetRatioNew( double ratio)
        {
            Ratio = ratio;
         
        }


        public void Reset()
        {
            Timer = 0;
            CurrentIndex = 0;
        }
        
        
    }
}