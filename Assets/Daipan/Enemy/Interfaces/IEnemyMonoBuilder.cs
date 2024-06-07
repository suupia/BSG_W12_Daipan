#nullable enable
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyMonoBuilder
    {
        EnemyMono Build(EnemyEnum enemyEnum, Vector3 position, Quaternion rotation);
    }
}