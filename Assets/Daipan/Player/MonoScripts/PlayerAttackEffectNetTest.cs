#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Core.Interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Player.Scripts;
using Fusion;
using UnityEngine;
using R3;

namespace Daipan.Player.MonoScripts
{
    public sealed class PlayerAttackEffectNetTest : NetworkBehaviour
    {
        public GameObject GameObject => gameObject;
        public Transform Transform => transform;
        [SerializeField] NetworkMecanimAnimator animatorNet = null!;
        [SerializeField] RuntimeAnimatorController testAnimator = null!;

        void Awake()
        {
            if (animatorNet == null) Debug.LogWarning("animatorNet is null");

            SetDomainTest();
        }

        public void SetDomainTest()
        {
            // 引数でIPlayerParamDataを受け取る代わりに、SerializeFieldのAnimatorを使う
            animatorNet.Animator.runtimeAnimatorController = testAnimator;
        }

        public void Hit(Action onHit)
        {
            Debug.Log("SetTrigger isHit");
            animatorNet.SetTrigger("isHit");
        }
    }
}