#nullable enable
namespace Daipan.Enemy.Interfaces
{
    public interface IFinalBossParamData : IEnemyParamData
    {
        double GetSummonActionIntervalSec() => 1;
        int GetSummonEnemyCount() => 5;
        double GetSummonEnemyIntervalSec() => 1;
        double GetDaipanHitDamagePercent() => 10;
        double GetKnockBackDistance() => 1;
        int GetCommentCount() => 5;
    } 
}

