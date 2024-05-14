#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.MonoScripts;
using UnityEngine;

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

        public EnemyMono NearestEnemy(Vector3 position)
        {
            // [Prerequisite] The enemy list is not empty
            if (!_enemies.Any())
            {
                Debug.LogWarning("No enemies found");
                return null;
            }

            var minDistance = float.MaxValue;
            var result = _enemies.First();
            foreach (var enemy in _enemies)
                if ((position - enemy.transform.position).sqrMagnitude < minDistance)
                {
                    minDistance = (position - enemy.transform.position).sqrMagnitude;
                    result = enemy;
                }

            return result;
        }
    }
}