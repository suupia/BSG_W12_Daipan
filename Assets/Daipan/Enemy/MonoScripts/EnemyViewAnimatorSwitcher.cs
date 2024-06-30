#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Daipan.Utility.Scripts;
using R3;
using UnityEngine;

namespace Daipan.Enemy.MonoScripts
{
    public class EnemyViewAnimatorSwitcher
    {
        readonly IEnumerable<Animator> _animators;
        readonly Animator _leaderAnimator;
        readonly HpGaugeMono _hpGaugeMono;
        readonly SpriteRenderer _highlightSpriteRenderer;

        bool _canHighlight = true;


        public EnemyViewAnimatorSwitcher(
            IEnumerable<Animator> animators,
            Animator leaderAnimator,
            HpGaugeMono hpGaugeMono,
            SpriteRenderer highlightSpriteRenderer
        )
        {
            _animators = animators;
            _leaderAnimator = leaderAnimator;
            _highlightSpriteRenderer = highlightSpriteRenderer;
            _hpGaugeMono = hpGaugeMono;

            _highlightSpriteRenderer.enabled = false;
        }


        public void SetHpGauge(int currentHp, int maxHp)
        {
            _hpGaugeMono.SetRatio(currentHp / (float)maxHp);
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

        public void Died(Action onDied)
        {
            SetTriggerAll("OnDied");
            _highlightSpriteRenderer.enabled = false;
            _canHighlight = false;
            // _leaderAnimatorを代表とする
            var preState = _leaderAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
            Observable.EveryValueChanged(_leaderAnimator, a => a.IsEnd())
                .Where(_ => preState != _leaderAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash)
                .Where(isEnd => isEnd)
                .Subscribe(_ => onDied())
                .AddTo(_leaderAnimator.gameObject);
        }

        public void Daipaned(Action onDied)
        {
            SetTriggerAll("OnDaipaned");
            _highlightSpriteRenderer.enabled = false;
            _canHighlight = false;
            // _leaderAnimatorを代表とする
            var preState = _leaderAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
            Observable.EveryValueChanged(_leaderAnimator, a => a.IsEnd())
                .Where(_ => preState != _leaderAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash)
                .Where(isEnd => isEnd)
                .Subscribe(_ => onDied())
                .AddTo(_leaderAnimator.gameObject);
        }

        public void Highlight(bool isHighlighted)
        {
            if (!_canHighlight) return;
            _highlightSpriteRenderer.enabled = isHighlighted;
        }

        public void SetTriggerAll(string paramName)
        {
            foreach (var animator in _animators) animator.SetTrigger(paramName);
        }

        public void SetBoolAll(string paramName, bool value)
        {
            foreach (var animator in _animators) animator.SetBool(paramName, value);
        }
    }
}