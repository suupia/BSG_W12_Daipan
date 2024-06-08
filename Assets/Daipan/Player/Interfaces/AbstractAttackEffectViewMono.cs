#nullable enable
using UnityEngine;

namespace Daipan.Player.Interfaces
{
    public abstract class AbstractAttackEffectViewMono : MonoBehaviour
    {
        public abstract void Idle();
        public abstract void Hit();
    }
}