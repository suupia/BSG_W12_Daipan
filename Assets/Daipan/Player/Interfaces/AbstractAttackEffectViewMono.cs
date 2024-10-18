#nullable enable
using System;
using UnityEngine;

namespace Daipan.Player.Interfaces
{
    public abstract class AbstractAttackEffectViewMono : MonoBehaviour, IAttackEffectViewMono
    {
        public abstract void Hit(Action onHit);
    }
}