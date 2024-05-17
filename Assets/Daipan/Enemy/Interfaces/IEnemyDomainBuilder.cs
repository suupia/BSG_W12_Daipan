#nullable enable
using Daipan.Enemy.MonoScripts;
namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyDomainBuilder
    {
        EnemyMono SetDomain(EnemyMono enemyMono);
    }
}