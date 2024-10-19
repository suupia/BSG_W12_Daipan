#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Enemy.Interfaces;
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;
using Daipan.Utility.Scripts;
using Fusion;
using R3;
using UnityEngine;

namespace Daipan.Player.MonoScripts
{
    public sealed class PlayerAttackEffectViewNet : NetworkBehaviour, IAttackEffectViewMono
    {
        [SerializeField] NetworkMecanimAnimator animatorNet = null!;
        void Awake()
        {   
            if(animatorNet == null) Debug.LogWarning("animatorNet is null");
        }
        public void SetDomain(IPlayerParamData playerParamData)
        {
            animatorNet.Animator.runtimeAnimatorController = playerParamData.GetAnimator();
        }

        public void Hit(Action onHit)
        {
            Debug.Log("HitNew");
            animatorNet.SetTrigger("isHit"); 
            var preState = animatorNet.Animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
            Observable.EveryValueChanged(animatorNet.Animator, a => a.IsAlmostEnd())
                .Where(isEnd => isEnd)
                .Where(_ => preState != animatorNet.Animator.GetCurrentAnimatorStateInfo(0).fullPathHash)
                .Subscribe(_ => onHit())
                .AddTo(animatorNet.Animator.gameObject);
        }
    }
}