#nullable enable
using System;
using Daipan.Player.Interfaces;
using UnityEngine;

namespace Daipan.Player.MonoScripts
{
    public class PlayerViewMono : AbstractPlayerViewMono
    {
        [SerializeField] Animator animator = null!;

        void Awake()
        {
            if (animator == null) Debug.LogWarning("animator is null");
        }
        
        
        public override void Idle()
        {
            animator.SetBool("IsIdling", true);
        }
    }
}