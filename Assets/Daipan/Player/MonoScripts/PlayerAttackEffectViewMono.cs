#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Enemy.Interfaces;
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;
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
            animator.SetTrigger("OnHit");
        }
    }
}