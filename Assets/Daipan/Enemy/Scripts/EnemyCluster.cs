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

        public IEnumerable<EnemyMono> EnemyMonos => _enemies;

        public void AddEnemy(EnemyMono enemy)
        {
            _enemies.Add(enemy);
        }
        public void RemoveEnemy(EnemyMono enemy)
        {
            _enemies.Remove(enemy);
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

        public void BlownAway()
        {
            var enemies = _enemies.ToArray();
            foreach (var enemy in enemies)
            {
                enemy.BlownAway();
                
            }
        }
    }
}