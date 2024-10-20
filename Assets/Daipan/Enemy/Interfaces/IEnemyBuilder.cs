#nullable enable
using System;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using JetBrains.Annotations;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyBuilder
    {
        [MustUseReturnValue]
        Action<IEnemyMono> BuildAction(IEnemySetDomain enemySetDomain, EnemyEnum enemyEnum);
    }
}