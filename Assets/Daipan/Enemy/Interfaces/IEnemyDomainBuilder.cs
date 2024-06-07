#nullable enable
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyDomainBuilder
    {
        EnemyMono SetDomain(NewEnemyType enemyEnum, EnemyMono enemyMono);
    }
}