#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Scripts;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyParamDataContainer
    {
        readonly IEnumerable<EnemyParamData> _enemyParamDataList;
        public EnemyParamDataContainer(IEnumerable<EnemyParamData> enemyParamDataList)
        {
            _enemyParamDataList = enemyParamDataList;
        }
        public EnemyParamData GetEnemyParamData(NewEnemyType enemyEnum)
        {
            return _enemyParamDataList.First(x => x.EnemyEnum() == enemyEnum);
        }
    } 
}

