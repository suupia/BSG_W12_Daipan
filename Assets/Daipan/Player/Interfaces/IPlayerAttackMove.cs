#nullable enable
using System;
using Daipan.Enemy.MonoScripts;
using Daipan.Player.MonoScripts;

namespace Daipan.Player.Interfaces
{
    public interface IPlayerAttackMove
    {
        event EventHandler<OnHitEventArgs>? OnHit;
        void Move();
    } 
    public record OnHitEventArgs(EnemyMono? EnemyMono, bool IsTargetEnemy);

}

