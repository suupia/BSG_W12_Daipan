#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Player.MonoScripts;

namespace Daipan.Player.Interfaces
{
    public interface IPlayerAttackMove
    {
        event EventHandler<OnHitEventArgs>? OnHit;
        void Move();
    } 
    public record OnHitEventArgs(AbstractEnemyMono? EnemyMono, bool IsTargetEnemy);

}

