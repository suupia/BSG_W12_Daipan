#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.MonoScripts;
using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyCluster
    {
        readonly List<EnemyMono> _enemies = new();

        public IEnumerable<EnemyMono> EnemyMonos => _enemies;

        public void Add(EnemyMono enemy)
        {
            _enemies.Add(enemy);
        }

        public void Remove(EnemyMono enemy, bool isDaipaned = false, bool isTriggerCallback = true)
        {
            _enemies.Remove(enemy);
            enemy.Died(isDaipaned, isTriggerCallback);
        }

        public EnemyMono? NearestEnemy(EnemyEnum enemyEnum, Vector3 position)
        {
            // [Precondition] The enemy list is not empty
            if (!_enemies.Any())
            {
                // Debug.LogWarning("No enemies found");
                return null;
            }

            var minDistance = float.MaxValue;
            var result = _enemies.FirstOrDefault(e => e.EnemyEnum == enemyEnum);
            foreach (var enemy in _enemies)
                if ((position - enemy.transform.position).sqrMagnitude < minDistance)
                {
                    minDistance = (position - enemy.transform.position).sqrMagnitude;
                    result = enemy;
                }

            return result;
        }

        public void Daipaned(float probability = 1.0f)
        {
            var enemies = _enemies.ToArray();
            foreach (var enemy in enemies)
                if (Random.value < probability)
                    Remove(enemy, isDaipaned:true);
        }

        public void Daipaned(Func<EnemyEnum, bool> blowAwayCondition)
        {
            var enemies = _enemies.ToArray();
            foreach (var enemy in enemies)
                if (blowAwayCondition(enemy.EnemyEnum))
                    Remove(enemy, isDaipaned:true);
        }
    }
}