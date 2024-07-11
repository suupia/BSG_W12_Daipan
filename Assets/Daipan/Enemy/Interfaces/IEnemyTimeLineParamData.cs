#nullable enable
namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyTimeLineParamData
    {
        int GetSpawnEnemyCount();
        double GetSpawnIntervalSec();
        double GetMoveSpeedRate();
        double GetSpawnBossPercent();
        double GetSpawnSpecialPercent();
    }
}