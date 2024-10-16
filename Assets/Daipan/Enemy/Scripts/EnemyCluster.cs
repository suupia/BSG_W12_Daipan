#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;


namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyCluster
    {
        public IEnumerable<IEnemyMono?> Enemies => _enemies;
        readonly List<IEnemyMono?> _enemies = new();
        readonly Queue<IEnemyMono?> _reachedPlayer = new();

        public void Add(IEnemyMono enemy)
        {
            _enemies.Add(enemy);
        }

        public void Remove(IEnemyMono enemy)
        {
            // _enemiesリストからenemyを削除
            _enemies.Remove(enemy);

            // _reachedPlayerキューからenemyを削除
            Queue<IEnemyMono?> newQueue = new Queue<IEnemyMono?>();
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
            
        }

        
        public IEnemyMono? NearestEnemy(Vector3 position)
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
                enemy.Highlight(enemy == orderedEnemies.First());
            }

        }

        public void Daipaned()
        {
            var enemies = _enemies.ToArray();
            foreach (var enemy in enemies)
            {
                if(enemy == null) continue;
                enemy.OnDaipaned(); 
            }
        }

        static Queue<IEnemyMono?> UpdateReachedPlayer(
            List<IEnemyMono?> enemies
            ,Queue<IEnemyMono?> reachedPlayer
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

        static List<IEnemyMono?> CalcOrderedEnemy(
            List<IEnemyMono?> enemies
            ,Queue<IEnemyMono?> reachedPlayer
            ,Vector3 position
            )
        {
            // reachedPlayerキューを更新
            var newReachedPlayer = UpdateReachedPlayer(enemies, reachedPlayer);
            
            // reachedPlayerキューをリストに変換
            List<IEnemyMono?> orderedEnemies = newReachedPlayer.ToList();

            // enemiesリストをソートし、orderedEnemiesリストに追加
            orderedEnemies.AddRange(enemies.OrderBy(e => Distance(e, position)));
            
            return orderedEnemies;
        }
        static float Distance(IEnemyMono? enemyMono, Vector3 position) => enemyMono == null ? float.MaxValue : (position - enemyMono.Transform.position).sqrMagnitude;
        
    }
}