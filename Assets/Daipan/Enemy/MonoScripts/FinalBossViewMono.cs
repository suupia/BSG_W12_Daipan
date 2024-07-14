#nullable enable
using System.Collections.Generic;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Daipan.Utility.Scripts;
using R3;
using UnityEngine;

namespace Daipan.Enemy.MonoScripts
{
    public sealed class FinalBossViewMono : AbstractFinalBossViewMono 
    {
        [SerializeField] HpGaugeMono hpGaugeMono = null!;
        [SerializeField] SpriteRenderer tempSpriteRenderer = null!; // todo: 完成時には削除する
        [SerializeField] Animator animatorHighlight = null!;
        [SerializeField] Animator animatorBody = null!;
        [SerializeField] Animator animatorEye = null!;
        [SerializeField] Animator animatorEyeBall = null!;
        [SerializeField] Animator animatorLine = null!;
        [SerializeField] Animator animatorGekiha = null!;
        [SerializeField] SpriteRenderer highlightSpriteRenderer = null!;
        Animator _leaderAnimator = null!;
        HpGaugeMono _hpGaugeMono = null!;
        SpriteRenderer _highlightSpriteRenderer = null!;
        IEnumerable<Animator> _animators = null!;
        bool _canHighlight = true;
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

            _animators =
                new[] { animatorHighlight, animatorBody, animatorEye, animatorEyeBall, animatorLine };
            _leaderAnimator = animatorLine;
            _highlightSpriteRenderer = highlightSpriteRenderer;
            _hpGaugeMono = hpGaugeMono;
            
            _highlightSpriteRenderer.enabled = false;
        }
        public override void SetDomain(IFinalBossViewParamData enemyParamData)
        {
            Debug.Log("SetDomain FinalBossViewMono");
            
            Observable.EveryValueChanged(enemyParamData, p=> p.GetBodyColor())
                .Subscribe(x => animatorBody.GetComponent<SpriteRenderer>().color = x)
                .AddTo(this);
            Observable.EveryValueChanged(enemyParamData, p=> p.GetLineColor())
                .Subscribe(x => animatorLine.GetComponent<SpriteRenderer>().color = x)
                .AddTo(this);
            Observable.EveryValueChanged(enemyParamData, p=> p.GetEyeColor())
                .Subscribe(x => animatorEye.GetComponent<SpriteRenderer>().color = x)
                .AddTo(this);
            Observable.EveryValueChanged(enemyParamData, p=> p.GetEyeBallColor())
                .Subscribe(x => animatorEyeBall.GetComponent<SpriteRenderer>().color = x)
                .AddTo(this);
            
            tempSpriteRenderer.color = EnemyViewTempColor.GetTempColor(EnemyEnum.YellowBoss);
            
        }

        public override void SetHpGauge(double currentHp, int maxHp)
        {
           _hpGaugeMono.SetRatio((float)currentHp / maxHp); 
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

        public override void SummonEnemy()
        {
            // todo: 実装
            // SetBoolAll("IsMoving", false);
            // SetBoolAll("IsAttacking", true); 
        }
        public override void Died(System.Action onDied)
        {
            SetTriggerAll("OnDied");
            _highlightSpriteRenderer.enabled = false;
            _canHighlight = false;
            // _leaderAnimatorを代表とする
            var preState = _leaderAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
            Observable.EveryValueChanged(_leaderAnimator, a => a.IsAlmostEnd())
                .Where(isEnd => isEnd)
                .Where(_ => preState != _leaderAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash)
                .Subscribe(_ => onDied())
                .AddTo(_leaderAnimator.gameObject);
            
            // Gekiha
            animatorGekiha.SetTrigger("OnGekiha");
        }

        public override void Daipaned(System.Action onDied)
        {
            SetTriggerAll("OnDaipaned");
            _highlightSpriteRenderer.enabled = false;
            _canHighlight = false;
            // _leaderAnimatorを代表とする
            var preState = _leaderAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
            Observable.EveryValueChanged(_leaderAnimator, a => a.IsAlmostEnd())
                .Where(isEnd => isEnd)
                .Where(_ => preState != _leaderAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash)
                .Subscribe(_ => onDied())
                .AddTo(_leaderAnimator.gameObject);
        }
        
        public override void DaipanHit()
        {
            SetTriggerAll("OnDaipanHit");
        }

        public override void Highlight(bool isHighlighted)
        {
            if (!_canHighlight) return;
            _highlightSpriteRenderer.enabled = isHighlighted;
        }
        void SetTriggerAll(string paramName)
        {
            foreach (var animator in _animators) animator.SetTrigger(paramName);
        }

        void SetBoolAll(string paramName, bool value)
        {
            foreach (var animator in _animators) animator.SetBool(paramName, value);
        }
    } 
}

