#nullable enable
using System.Collections.Generic;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Daipan.Utility.Scripts;
using R3;
using UnityEngine;

namespace Daipan.Enemy.MonoScripts
{
    public sealed class FinalBossViewMono : MonoBehaviour 
    {
        [SerializeField] HpGaugeMono hpGaugeMono = null!;
        [SerializeField] SpriteRenderer tempSpriteRenderer = null!; // todo: 完成時には削除する
        [SerializeField] Animator animatorHighlight = null!;
        [SerializeField] Animator animatorBody = null!;
        [SerializeField] Animator animatorEye = null!;
        [SerializeField] Animator animatorEyeBall = null!;
        [SerializeField] Animator animatorLine = null!;
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
        public void SetDomain(IFinalBossViewParamData enemyParamData)
        {
            Debug.Log("SetDomain FinalBossViewMono");
            animatorBody.GetComponent<SpriteRenderer>().color = enemyParamData.GetBodyColor();
            animatorLine.GetComponent<SpriteRenderer>().color = enemyParamData.GetLineColor();
            animatorEye.GetComponent<SpriteRenderer>().color = enemyParamData.GetEyeColor();
            animatorEyeBall.GetComponent<SpriteRenderer>().color = enemyParamData.GetEyeBallColor();
            
            tempSpriteRenderer.color = EnemyViewTempColor.GetTempColor(EnemyEnum.YellowBoss);
            
        }

        public void SetHpGauge(double currentHp, int maxHp)
        {
           _hpGaugeMono.SetRatio((float)currentHp / maxHp); 
        }

        public void Move()
        {
            SetBoolAll("IsMoving", true);
            SetBoolAll("IsAttacking", false);
        }

        public void Attack()
        {
            SetBoolAll("IsMoving", false);
            SetBoolAll("IsAttacking", true);
        }

        public void SummonEnemy()
        {
            // todo: 実装
            // SetBoolAll("IsMoving", false);
            // SetBoolAll("IsAttacking", true); 
        }
        public void Died(System.Action onDied)
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
        }

        public void Daipaned(System.Action onDied)
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

        public void Highlight(bool isHighlighted)
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
