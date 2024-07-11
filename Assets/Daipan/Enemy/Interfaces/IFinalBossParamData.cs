#nullable enable
namespace Daipan.Enemy.Interfaces
{
    public interface IFinalBossParamData : IEnemyParamData
    {
        double GetSummonActionIntervalSec() => 1;
        int GetSummonCount() => 5;
        double GetSummonEnemyIntervalSec() => 1;
    } 
}

