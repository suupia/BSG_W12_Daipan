#nullable enable
using System.Collections.Generic;
using Daipan.Enemy.MonoS;

namespace Daipan.Enemy.Scripts
{
    public class EnemyCluster
    {
        readonly List<EnemyMono> _enemies = new();
        public IEnumerable<EnemyMono> EnemyMonos => _enemies;
    }
}