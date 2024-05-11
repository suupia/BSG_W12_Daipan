#nullable enable
using System.Collections.Generic;

namespace Enemy
{
    public class EnemyCluster
    {
        readonly List<EnemyMono> _enemies = new();
        public IEnumerable<EnemyMono> EnemyMonos => _enemies;


        public void AddEnemy(EnemyMono enemy)
        {
            _enemies.Add(enemy);
        }
}
}