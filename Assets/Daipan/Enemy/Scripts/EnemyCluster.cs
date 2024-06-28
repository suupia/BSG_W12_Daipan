#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.MonoScripts;
using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyCluster
    {
        readonly List<EnemyMono?> _enemies = new();

        public void Add(EnemyMono enemy)
        {
            _enemies.Add(enemy);
        }

        public void Remove(EnemyMono enemy)
        {
            _enemies.Remove(enemy);
            enemy.Died();
        }

        public EnemyMono? NearestEnemy(EnemyEnum enemyEnum, Vector3 position)
        {
            if (!_enemies.Any())
            {
                // Debug.LogWarning("No enemies found");
                return null;
            }

            return _enemies
                .Where(e => e?.EnemyEnum == enemyEnum)
                .OrderBy(e => (position - e?.transform.position)?.sqrMagnitude)
                .FirstOrDefault();
        }
        
        public EnemyMono? NearestEnemy(Vector3 position)
        {
            if (!_enemies.Any())
            {
                // Debug.LogWarning("No enemies found");
                return null;
            }
            
            return _enemies
                .OrderBy(e => (position - e?.transform.position)?.sqrMagnitude)
                .FirstOrDefault();
            
        }
        
        public void UpdateHighlight(Vector3 position)
        {
            if (!_enemies.Any())
            {
                // Debug.LogWarning("No enemies found");
                return;
            }
            
            var orderedEnemies = _enemies
                .OrderBy(e => (position - e?.transform.position)?.sqrMagnitude)
                .ToArray();
            
            // 先頭のenemyはハイライトしそうでないenemyはハイライトしない
            foreach (var enemy in orderedEnemies)
            {
                if(enemy == null) continue;
                if (enemy == orderedEnemies.First())
                {
                    SwitchHighlight(enemy, isHighlighted: true);
                }
                else
                {
                    SwitchHighlight(enemy, isHighlighted: false);
                }
            }
        }

        public void Daipaned()
        {
            var enemies = _enemies.ToArray();
            foreach (var enemy in enemies)
                    enemy.Died(isDaipaned:true); 
        }

        public void Daipaned(Func<EnemyEnum, bool> blowAwayCondition)
        {
            var enemies = _enemies.ToArray();
            foreach (var enemy in enemies)
                if (blowAwayCondition(enemy.EnemyEnum))
                    enemy.Died(isDaipaned:true); 
        }
        
        static void SwitchHighlight(EnemyMono enemyMono, bool isHighlighted)
        {
            var enemyViewMono = enemyMono.EnemyViewMono;
            if (enemyViewMono == null) return;
            enemyViewMono.Highlight(isHighlighted);
        }
        
    }
}