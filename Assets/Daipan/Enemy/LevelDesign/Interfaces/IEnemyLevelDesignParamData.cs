#nullable enable
namespace Daipan.Enemy.LevelDesign.Interfaces
{
    public interface IEnemyLevelDesignParamData
    {
         int GetIncreaseViewerOnEnemyKill() => 5;
         int GetIncreaseIrritationGaugeOnSpecialEnemyKill() => 5;
         int CurrentKillAmount { get; set; } 
    } 
}
