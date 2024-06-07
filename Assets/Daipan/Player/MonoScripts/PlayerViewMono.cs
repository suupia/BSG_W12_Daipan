#nullable enable
using System;
using UnityEngine;

namespace Daipan.Player.MonoScripts
{
    public class PlayerViewMono : MonoBehaviour
    {
        [SerializeField] Animator animator = null!;

        void Awake()
        {
            if (animator == null) Debug.LogWarning("animator is null");
        }

        public void Idle()
        {
            animator.SetBool("IsIdling", true);
        }
    }
}