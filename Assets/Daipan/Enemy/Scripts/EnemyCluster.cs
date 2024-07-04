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
        readonly Queue<EnemyMono?> _reachedPlayer = new();

        public void Add(EnemyMono enemy)
        {
            _enemies.Add(enemy);
        }

        public void Remove(EnemyMono enemy, bool isDaipaned = false)
        {
            // _enemiesリストからenemyを削除
            _enemies.Remove(enemy);

            // _reachedPlayerキューからenemyを削除
            Queue<EnemyMono?> newQueue = new Queue<EnemyMono?>();
            while (_reachedPlayer.Count > 0)
            {
                var dequeuedEnemy = _reachedPlayer.Dequeue();
                if (dequeuedEnemy != enemy)
                {
                    newQueue.Enqueue(dequeuedEnemy);
                }
            }
            // 新しいキューを元のキューに置き換える
            while (newQueue.Count > 0)
            {
                _reachedPlayer.Enqueue(newQueue.Dequeue());
            }
            
            enemy.Died(isDaipaned);
        }

        
        public EnemyMono? NearestEnemy(Vector3 position)
        {
             var orderedEnemies = CalcOrderedEnemy( _enemies, _reachedPlayer, position);
             return orderedEnemies.FirstOrDefault();
        }
        
        public void UpdateHighlight(Vector3 position)
        {
            var orderedEnemies = CalcOrderedEnemy( _enemies, _reachedPlayer, position);
            
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
            {
                if(enemy == null) continue;
                enemy.Remove(enemy,isDaipaned:true); 
            }
        }

        public void Daipaned(Func<EnemyEnum, bool> blowAwayCondition)
        {
            var enemies = _enemies.ToArray();
            foreach (var enemy in enemies)
            {
                if (enemy == null) continue;
                if (blowAwayCondition(enemy.EnemyEnum))
                    enemy.Remove(enemy, true);
            }

        }
        
        static void SwitchHighlight(EnemyMono enemyMono, bool isHighlighted)
        {
            var enemyViewMono = enemyMono.EnemyViewMono;
            if (enemyViewMono == null) return;
            enemyViewMono.Highlight(isHighlighted);
        }
        
        static Queue<EnemyMono?> UpdateReachedPlayer(
            List<EnemyMono?> enemies
            ,Queue<EnemyMono?> reachedPlayer
            )
        {
            foreach (var enemy in enemies)
            {
                if(enemy== null) continue;
                if(enemy.IsReachedPlayer == false) continue;
                if(!reachedPlayer.Contains(enemy)) reachedPlayer.Enqueue(enemy);
            } 
            return reachedPlayer;
        }

        static List<EnemyMono?> CalcOrderedEnemy(
            List<EnemyMono?> enemies
            ,Queue<EnemyMono?> reachedPlayer
            ,Vector3 position
            )
        {
            // reachedPlayerキューを更新
            var newReachedPlayer = UpdateReachedPlayer(enemies, reachedPlayer);
            
            // reachedPlayerキューをリストに変換
            List<EnemyMono?> orderedEnemies = newReachedPlayer.ToList();

            // enemiesリストをソートし、orderedEnemiesリストに追加
            orderedEnemies.AddRange(enemies.OrderBy(e => Distance(e, position)));
            
            
            // Debug
            Debug.Log ("Ordered Enemies");  
            foreach (var enemy in orderedEnemies)
            {
                if(enemy == null) continue;
                Debug.Log($"enemy: {enemy.EnemyEnum} distance: {Distance(enemy, position)}");
            }

            return orderedEnemies;
        }
        static float Distance(EnemyMono? enemyMono, Vector3 position) => enemyMono == null ? float.MaxValue : (position - enemyMono.transform.position).sqrMagnitude;
        
    }
}