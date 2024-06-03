#nullable enable
using UnityEngine;

namespace Daipan.Utility.Scripts
{
    public static class AnimatorExtension
    {
        public static bool IsEnd(this Animator animator, int layerIndex = 0)
        {
            // When the animator is not active, it is considered to be in the end state.
            if (!animator.gameObject.activeInHierarchy)
                return true;
            var stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
            return stateInfo.normalizedTime >= 1;
        }
    }

}