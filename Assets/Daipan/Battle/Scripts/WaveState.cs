#nullable enable
using Daipan.Enemy.Interfaces;

namespace Daipan.Battle.scripts
{
    public class WaveState
    {
        public int CurrentWave => _enemyTimeLineParamContainer.GetEnemyTimeLineParamDataIndex();

        readonly IEnemyTimeLineParamContainer _enemyTimeLineParamContainer;
        
        public WaveState(IEnemyTimeLineParamContainer enemyTimeLineParamContainer)
        {
            _enemyTimeLineParamContainer = enemyTimeLineParamContainer;
        }
    }
}