#nullable enable
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyMonoBuilder
    {
        EnemyMono Build(NewEnemyType enemyEnum, Vector3 position, Quaternion rotation);
    }
}