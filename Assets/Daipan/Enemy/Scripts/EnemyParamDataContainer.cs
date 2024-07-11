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
            , IEnemyTimeLineParamContainer enemyTimeLineParamContainer
            )
        {
            _enemyParamDatas = CreateEnemyParamData(enemyParamsManager, enemyTimeLineParamContainer);
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
            , IEnemyTimeLineParamContainer enemyTimeLineParamDataContainer
           ) 
        {
            var enemyParams = new List<EnemyParamData>();
            foreach (var enemyParam in enemyParamsManager.enemyParams)
                enemyParams.Add(new EnemyParamData(enemyParam, enemyTimeLineParamDataContainer));
            return enemyParams;
        }

        static EnemyTimeLineParamData GetEnemyTimeLineParam(
            IEnemyTimeLineParamContainer enemyTimeLineParamDataContainer)
        {
            return enemyTimeLineParamDataContainer.GetEnemyTimeLineParamData();
        }
    }
}