#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Scripts;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyTimeLineParamDataContainer
    {
        readonly IEnumerable<EnemyTimeLineParamData> _enemyParamDataList;

        public EnemyTimeLineParamDataContainer(IEnumerable<EnemyTimeLineParamData> enemyParamDataList)
        {
            _enemyParamDataList = enemyParamDataList;
        }

        public EnemyTimeLineParamData GetEnemyParamData(EnemyEnum enemyEnum)
        {
            // return _enemyParamDataList.First(x => x.EnemyEnum() == enemyEnum);
            return null!;
        }
    }
}