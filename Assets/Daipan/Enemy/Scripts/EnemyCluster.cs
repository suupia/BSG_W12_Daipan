#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.MonoScripts;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Daipan.Enemy.Scripts
{
    public class EnemyCluster
    {
        readonly List<EnemyMono> _enemies = new();

        public IEnumerable<EnemyMono> EnemyMonos => _enemies;

        public void Add(EnemyMono enemy)
        {
            _enemies.Add(enemy);
        }

        public void Remove(EnemyMono enemy)
        {
            _enemies.Remove(enemy);
            enemy.Died();
        }

        public EnemyMono NearestEnemy(Vector3 position)
        {
            // [Prerequisite] The enemy list is not empty
            if (!_enemies.Any())
            {
                Debug.LogWarning("No enemies found");
                return null!;
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

        public void BlownAway(float probability = 1.0f)
        {
            var enemies = _enemies.ToArray();
            foreach (var enemy in enemies)
                if (Random.value < probability)
                    Remove(enemy);
        }

        public void BlownAway(Func<EnemyEnum, bool> blowAwayCondition)
        {
            var enemies = _enemies.ToArray();
            foreach (var enemy in enemies)
                if (blowAwayCondition(enemy.EnemyParameter.GetEnemyEnum))
                    Remove(enemy);
        }
    }
}