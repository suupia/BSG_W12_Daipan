#nullable enable
using Daipan.Enemy.MonoScripts;
using UnityEngine;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyMonoBuilder
    {
        EnemyMono Build(Vector3 position, Quaternion rotation);
    }
}