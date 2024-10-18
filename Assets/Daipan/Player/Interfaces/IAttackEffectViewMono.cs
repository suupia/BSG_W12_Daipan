#nullable enable
using System;
using Daipan.Player.LevelDesign.Interfaces;

namespace Daipan.Player.Interfaces;

public interface IAttackEffectViewMono
{
    public void SetDomain(IPlayerParamData playerParamData);
    public void Hit(Action onHit);
}