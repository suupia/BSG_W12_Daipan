#nullable enable
using System;
using Daipan.Player.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.Player.MonoScripts
{
    public class PlayerViewMono : AbstractPlayerViewMono
    {
        [SerializeField] public PlayerColor PlayerColor;
        [SerializeField] Animator animator = null!;

        public override void Idle()
        {
            animator.SetBool("IsIdling", true);
        }
        public override void Damage()
        {
            animator.SetTrigger("OnDamage");
        }
    }
    
    public enum PlayerColor
    {
        Red,
        Blue,
        Yellow,
    }
}