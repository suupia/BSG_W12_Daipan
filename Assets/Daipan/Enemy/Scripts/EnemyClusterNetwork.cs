#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Fusion;
using UnityEngine;


namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyClusterNetwork : IEnemyCluster 
    {
        public IEnumerable<IEnemyMono?> Enemies => _enemyCluster.Enemies;
        readonly IEnemyCluster _enemyCluster;
        readonly Dictionary<IEnemyMono, PlayerRef> _enemyParents = new();

        public EnemyClusterNetwork(IEnemyCluster enemyCluster)
        {
            _enemyCluster = enemyCluster;
        }

        public void Add(IEnemyMono enemy)
        {
            _enemyCluster.Add(enemy);
        }

        public void Remove(IEnemyMono enemy)
        {
            _enemyCluster.Remove(enemy);

            // _enemyParentsに登録されていれば削除
            Debug.Log($"This enemy is spawned by {GetPlayerRef(enemy)}");
            _enemyParents.Remove(enemy);
        }


        public IEnemyMono? NearestEnemy(Vector3 position)
        {
            return _enemyCluster.NearestEnemy(position);
        }

        public void UpdateHighlight(Vector3 position)
        {
            _enemyCluster.UpdateHighlight(position);
        }

        public void Daipaned()
        {
            _enemyCluster.Daipaned();
        }

        public void SetPlayerRef(IEnemyMono enemy, PlayerRef playerRef)
        {
            _enemyParents[enemy] = playerRef;
        }

        public PlayerRef GetPlayerRef(IEnemyMono enemy)
        {
            if (_enemyParents.TryGetValue(enemy, out PlayerRef playerRef))
            {
                return playerRef;
            }
            else return PlayerRef.None;
        }
    }
}