#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Enemy.Interfaces;
using Daipan.Player.Interfaces;
using UnityEngine;

namespace Daipan.Player.MonoScripts
{
    public class PlayerAttackEffectViewMono : AbstractAttackEffectViewMono
    {
        [SerializeField] Animator animator = null!;
        void Awake()
        {   
            if(animator == null) Debug.LogWarning("animator is null");
        }
        public void SetDomain(PlayerColor playerColor)
        {
            // animator.runtimeAnimatorController = playerColor.GetAnimator();
        }
        public override void Idle()
        {
            animator.SetBool("IsIdling", true);
        }

        public override void Hit()
        {
            animator.SetTrigger("OnHit");
        }
    }
}