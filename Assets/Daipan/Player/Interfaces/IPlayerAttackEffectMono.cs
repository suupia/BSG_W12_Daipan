#nullable enable
using System;
using Daipan.Core.Interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;

namespace Daipan.Player.Interfaces;

public interface IPlayerAttackEffectMono : IMonoBehaviour
{
    public event EventHandler<OnHitEventArgs>? OnHit;

    public void SetUp(IPlayerParamData playerParamData, Func<IEnemyMono?> getTargetEnemyMono);

    public void Defenced();
}