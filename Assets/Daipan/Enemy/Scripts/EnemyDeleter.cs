#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;

namespace Daipan.Enemy.Scripts;

public class EnemyDeleter : IEnemyDeleter
{
    readonly EnemyMono _enemyMono;
    
    public EnemyDeleter(EnemyMono enemyMono)
    {
        _enemyMono = enemyMono;
    }
    
    public void Delete()
    {
        UnityEngine.Object.Destroy(_enemyMono); 
    }
}