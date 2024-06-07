#nullable enable
using Daipan.Player.MonoScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.Player.Interfaces
{
    public abstract class AbstractPlayerViewMono : MonoBehaviour
    {
        public PlayerColor playerColor;
        public abstract void Idle();
        public abstract void Attack();
        public abstract void Damage();
    }
}