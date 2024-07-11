#nullable enable
namespace Daipan.Enemy.Interfaces
{
    public interface IFinalBossParamData : IEnemyParamData
    {
        double GetSummonIntervalSec() => 1;
    } 
}

