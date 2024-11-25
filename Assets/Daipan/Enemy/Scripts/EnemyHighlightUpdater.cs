#nullable enable
using Daipan.Core.Interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.LevelDesign.Enemy.Scripts;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyHighlightUpdater : IUpdate
    {
        readonly TowerParamsConfig _towerParamsConfig;
        readonly IEnemyCluster _enemyCluster;

        public EnemyHighlightUpdater(TowerParamsConfig towerParamsConfig, IEnemyCluster enemyCluster)
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
