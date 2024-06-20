#nullable enable
namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyTimeLineParamData
    {
        double GetStartTime();
        double GetSpawnIntervalSec();
        double GetMoveSpeedRate();
        double GetSpawnBossPercent();
        double GetSpawnSpecialPercent();
    }
}