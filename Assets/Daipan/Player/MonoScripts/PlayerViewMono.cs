#nullable enable
using System;
using Daipan.Player.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.Player.MonoScripts
{
    public class PlayerViewMono : AbstractPlayerViewMono
    {
        [SerializeField] Animator animator = null!;

        public override void Idle()
        {
            animator.SetBool("IsIdling", true);
        }

        public override void Attack()
        {
            Debug.Log("PlayerViewMono Attack");
            animator.SetTrigger("OnAttack");
        }

        public override void Damage()
        {
            animator.SetTrigger("OnDamaged");
        }
    }

    public enum PlayerColor
    {
        None,
        Red,
        Blue,
        Yellow
    }
}