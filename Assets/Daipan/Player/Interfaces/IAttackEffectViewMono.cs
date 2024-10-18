#nullable enable
using System;

namespace Daipan.Player.Interfaces;

public interface IAttackEffectViewMono
{
    public void Hit(Action onHit);
}