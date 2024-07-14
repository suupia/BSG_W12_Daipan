#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Enemy.Interfaces;
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;
using Daipan.Utility.Scripts;
using R3;
using UnityEngine;

namespace Daipan.Player.MonoScripts
{
    public sealed class PlayerAttackEffectViewMono : AbstractAttackEffectViewMono
    {
        [SerializeField] Animator animator = null!;
        void Awake()
        {   
            if(animator == null) Debug.LogWarning("animator is null");
        }
        public void SetDomain(IPlayerParamData playerParamData)
        {
            animator.runtimeAnimatorController = playerParamData.GetAnimator();
        }

        public override void Hit()
        {
            animator.SetTrigger("isHit");
        }
        public bool IsFinishAnimation()
        {
            Debug.Log($"animation {animator.GetCurrentAnimatorStateInfo(0).normalizedTime}");
            return animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1
                && !animator.IsInTransition(0);
        }

        public void HitNew(System.Action onHit)
        {
            Debug.Log("HitNew");
            animator.SetTrigger("isHit"); 
            var preState = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
            Observable.EveryValueChanged(animator, a => a.IsAlmostEnd())
                .Where(isEnd => isEnd)
                .Where(_ => preState != animator.GetCurrentAnimatorStateInfo(0).fullPathHash)
                .Subscribe(_ => onHit())
                .AddTo(animator.gameObject);
        }
    }
}