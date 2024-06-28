#nullable enable
using Daipan.Core.Interfaces;
using Daipan.LevelDesign.Enemy.Scripts;

namespace Daipan.Enemy.Scripts
{
    public class EnemyHighlightUpdater : IUpdate
    {
        readonly TowerParamsConfig _towerParamsConfig;
        readonly EnemyCluster _enemyCluster;
        
        public EnemyHighlightUpdater(TowerParamsConfig towerParamsConfig, EnemyCluster enemyCluster)
        {
            _towerParamsConfig = towerParamsConfig;
            _enemyCluster = enemyCluster;
        }
        
        void IUpdate.Update()
        {
            _enemyCluster.UpdateHighlight(_towerParamsConfig.GetTowerSpawnPosition());
        }
    }
 
}
