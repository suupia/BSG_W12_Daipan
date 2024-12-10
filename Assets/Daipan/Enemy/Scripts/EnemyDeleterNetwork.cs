#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Fusion;

namespace Daipan.Enemy.Scripts;

public class EnemyDeleterNetwork : IEnemyDeleter
{
    readonly EnemyNet _enemyNet;

    public EnemyDeleterNetwork(EnemyNet enemyNet)
    {
        _enemyNet = enemyNet;
    }
    
    public void Delete()
    {
        _enemyNet.Runner.Despawn(_enemyNet.Object);
    }
}