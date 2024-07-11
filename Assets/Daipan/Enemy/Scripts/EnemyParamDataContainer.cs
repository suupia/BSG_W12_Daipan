#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Stream.Scripts;
using VContainer;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyParamDataContainer : IEnemyParamContainer
    {
        readonly IEnumerable<EnemyParamData> _enemyParamDatas;
        [Inject]
        public EnemyParamDataContainer(
            EnemyParamsManager enemyParamsManager
            , IEnemyWaveParamContainer enemyWaveParamContainer
            )
        {
            _enemyParamDatas = CreateEnemyParamData(enemyParamsManager, enemyWaveParamContainer);
        }

        public IEnemyParamData GetEnemyParamData(EnemyEnum enemyEnum)
        {
            return _enemyParamDatas.First(x => x.GetEnemyEnum() == enemyEnum);
        }
        
        public IEnemyViewParamData GetEnemyViewParamData(EnemyEnum enemyEnum)
        {
            return  _enemyParamDatas.First(x => x.GetEnemyEnum() == enemyEnum);
        }

        static List<EnemyParamData> CreateEnemyParamData(
            EnemyParamsManager enemyParamsManager
            , IEnemyWaveParamContainer enemyWaveParamDataContainer
           ) 
        {
            var enemyParams = new List<EnemyParamData>();
            foreach (var enemyParam in enemyParamsManager.enemyParams)
                enemyParams.Add(new EnemyParamData(enemyParam, enemyWaveParamDataContainer));
            return enemyParams;
        }

        static EnemyWaveParamData GetEnemyTimeLineParam(
            IEnemyWaveParamContainer enemyWaveParamDataContainer)
        {
            return enemyWaveParamDataContainer.GetEnemyWaveParamData();
        }
    }
}