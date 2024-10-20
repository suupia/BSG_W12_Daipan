#nullable enable
using System;
using Daipan.Core.Interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;

namespace Daipan.Player.Interfaces;

public interface IPlayerAttackEffectMono : IMonoBehaviour
{
    public event EventHandler<OnHitEventArgs>? OnHit;
    public void Initialize(IPlayerParamDataContainer playerParamDataContainer);
    public void SetUp(PlayerColor playerColor, Func<IEnemyMono?> getTargetEnemyMono);

    public void Defenced();
}