#nullable enable
using System.Collections.Generic;
using Daipan.Enemy.MonoScripts;

namespace Daipan.Enemy.Scripts
{
    public class EnemyCluster
    {
        readonly List<EnemyMono> _enemies = new();

        EnemyCluster()
        {
        }

        public IEnumerable<EnemyMono> EnemyMonos => _enemies;

        public void AddEnemy(EnemyMono enemy)
        {
            _enemies.Add(enemy);
        }
    }
}