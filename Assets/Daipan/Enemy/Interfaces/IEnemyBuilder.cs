#nullable enable
using Daipan.Enemy.MonoScripts;
using UnityEngine;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyBuilder
    {
        EnemyMono Build(Vector3 position, Quaternion rotation);
    }
}