#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Scripts;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyParamWarpContainer
    {
        public IEnumerable<EnemyParamWarp> EnemyParamWarps => _enemyParamWarps;
        readonly IEnumerable<EnemyParamWarp> _enemyParamWarps;
        public EnemyParamWarpContainer(IEnumerable<EnemyParamWarp> enemyParamWarps)
        {
            _enemyParamWarps = enemyParamWarps;
        }
        public EnemyParamWarp GetEnemyParamData(EnemyEnum enemyEnum)
        {
            return _enemyParamWarps.First(x => x.GetEnemyEnum() == enemyEnum);
        }
    } 
}

