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
    public sealed class EnemyCluster : IEnemyCluster
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
            var orderedEnemies = CalcOrderedEnemy(_enemies, _reachedPlayer, position);
            return orderedEnemies.FirstOrDefault();
        }

        public void UpdateHighlight(Vector3 position)
        {
            var orderedEnemies = CalcOrderedEnemy(_enemies, _reachedPlayer, position);

            // 先頭のenemyはハイライトしそうでないenemyはハイライトしない
            foreach (var enemy in orderedEnemies)
            {
                if (enemy == null) continue;
                enemy.Highlight(enemy == orderedEnemies.First());
            }

        }

        public void Daipaned()
        {
            var enemies = _enemies.ToArray();
            foreach (var enemy in enemies)
            {
                if (enemy == null) continue;
                enemy.OnDaipaned();
            }
        }
        
        static List<IEnemyMono?> CalcOrderedEnemy(
            List<IEnemyMono?> enemies,
            Queue<IEnemyMono?> reachedPlayer,
            Vector3 position
        )
        {
            // reachedPlayerキューを更新
            var newReachedPlayer = UpdateReachedPlayer(enemies, reachedPlayer);

            // reachedPlayerキューをリストに変換
            var reachedEnemies = new List<IEnemyMono?>(newReachedPlayer);

            // FinalBossとそれ以外を分ける
            var finalBosses = reachedEnemies.Where(e => e?.EnemyEnum.IsFinalBoss() == true).ToList();
            var nonFinalBosses = reachedEnemies.Where(e => e?.EnemyEnum.IsFinalBoss() != true).ToList();

            // FinalBossを距離順にソート
            finalBosses = finalBosses.OrderBy(e => Distance(e, position)).ToList();

            // nonFinalBossesにFinalBossを適切な位置に挿入
            foreach (var finalBoss in finalBosses)
            {
                int insertIndex = nonFinalBosses.FindIndex(e => Distance(e, position) > Distance(finalBoss, position));
                if (insertIndex == -1)
                {
                    // 適切な位置が見つからない場合は末尾に追加
                    nonFinalBosses.Add(finalBoss);
                }
                else
                {
                    // 適切な位置に挿入
                    nonFinalBosses.Insert(insertIndex, finalBoss);
                }
            }

            // reachedEnemiesを更新済みリストとして使用
            var orderedEnemies = nonFinalBosses;

            // enemiesリストをソートし、orderedEnemiesリストに追加
            orderedEnemies.AddRange(enemies.Where(e => !reachedEnemies.Contains(e)).OrderBy(e => Distance(e, position)));

            return orderedEnemies;
        }

        
        static float Distance(IEnemyMono? enemyMono, Vector3 position) => enemyMono == null ? float.MaxValue : (position - enemyMono.Transform.position).sqrMagnitude;
        
        static Queue<IEnemyMono?> UpdateReachedPlayer(
            List<IEnemyMono?> enemies
            , Queue<IEnemyMono?> reachedPlayer
        )
        {
            foreach (var enemy in enemies)
            {
                if (enemy == null) continue;
                if (enemy.IsReachedPlayer == false) continue;
                if (!reachedPlayer.Contains(enemy)) reachedPlayer.Enqueue(enemy);
            }
            return reachedPlayer;
        }
    }
}