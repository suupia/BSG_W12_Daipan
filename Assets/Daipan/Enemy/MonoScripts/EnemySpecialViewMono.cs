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
    public sealed class EnemySpecialViewMono : AbstractEnemyViewMono
    {
        [SerializeField] HpGaugeMono hpGaugeMono = null!;
        [SerializeField] SpriteRenderer tempSpriteRenderer = null!; // todo: 完成時には削除する
        [SerializeField] Animator animatorHighlight = null!;
        [SerializeField] Animator animatorBody = null!;
        [SerializeField] Animator animatorEye = null!;
        [SerializeField] Animator animatorEyeBall = null!;
        [SerializeField] Animator animatorLine = null!;
        [SerializeField] Animator animatorSpecialBlackBody = null!;
        [SerializeField] Animator animatorSpecialBlackEye = null!;
        [SerializeField] SpriteRenderer highlightSpriteRenderer = null!;
        
        EnemyViewAnimatorSwitcher _animatorSwitcher = null!;
        bool IsPlayingSpecialBlack { get; set; }

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
                new [] {animatorHighlight, animatorBody, animatorEye, animatorEyeBall, animatorLine},
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
            animatorSpecialBlackBody.GetComponent<SpriteRenderer>().color = enemyViewParamData.GetLineColor();
            animatorSpecialBlackEye.GetComponent<SpriteRenderer>().color = enemyViewParamData.GetEyeColor(); 
            
            // temp
            tempSpriteRenderer.color = EnemyViewTempColor.GetTempColor(enemyViewParamData.GetEnemyEnum()); 

        }

        public override void SetHpGauge(double currentHp, int maxHp) => _animatorSwitcher.SetHpGauge(currentHp, maxHp);

        public override void Move() => _animatorSwitcher.Move();

        public override void Attack() => _animatorSwitcher.Attack();

        public override void Died(Action onDied)
        {
            if (IsPlayingSpecialBlack) return;
            _animatorSwitcher.Died(onDied);
        }

        public override void Daipaned(Action onDied) => _animatorSwitcher.Daipaned(onDied);
        public override void Highlight(bool isHighlighted) => _animatorSwitcher.Highlight(isHighlighted);
        
        public void SpecialBlack(Action onSpecialBlack)
        {
            Debug.Log("EnemySpecialViewMono.SpecialBlack()");
            IsPlayingSpecialBlack = true;
            animatorSpecialBlackBody.SetTrigger("SpecialBlack");
            animatorSpecialBlackEye.SetTrigger("SpecialBlack");
            
            // SpecialBlack以外のアニメーションは非表示にする
            animatorHighlight.gameObject.SetActive(false);
            animatorBody.gameObject.SetActive(false);
            animatorEye.gameObject.SetActive(false);
            animatorEyeBall.gameObject.SetActive(false);
            animatorLine.gameObject.SetActive(false);
            
            
            const double releaseSec = 1.5f;
            Observable.Timer(TimeSpan.FromSeconds(releaseSec))
                .Subscribe(_ =>
                {
                    IsPlayingSpecialBlack = false;
                    onSpecialBlack();
                });
        }
    }
}