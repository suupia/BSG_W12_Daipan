#nullable enable
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyDomainBuilder
    {
        EnemyMono SetDomain(EnemyEnum enemyEnum, EnemyMono enemyMono);
    }
}