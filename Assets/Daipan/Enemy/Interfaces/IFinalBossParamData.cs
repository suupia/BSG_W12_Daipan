#nullable enable
namespace Daipan.Enemy.Interfaces
{
    public interface IFinalBossParamData : IEnemyParamData
    {
        double GetSummonActionIntervalSec() => 1;
        int GetSummonEnemyCount() => 5;
        double GetSummonEnemyIntervalSec() => 1;
    } 
}

