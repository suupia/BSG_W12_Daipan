#nullable enable
using System.Linq;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Enemy.Scripts
{
    public sealed class FinalBossSpawnerNet
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<FinalBossNet> _finalBossMonoLoader;
        readonly EnemyCluster _enemyCluster;
        readonly IEnemySpawnPoint _enemySpawnPoint;
        readonly FinalBossBuilder _finalBossBuilder;
        
        public FinalBossSpawnerNet(
            IObjectResolver container
            , IPrefabLoader<FinalBossNet> finalBossMonoLoader
            , EnemyCluster enemyCluster
            , IEnemySpawnPoint enemySpawnPoint
            , FinalBossBuilder finalBossBuilder
        )
        {
            _container = container;
            _finalBossMonoLoader = finalBossMonoLoader;
            _enemyCluster = enemyCluster;
            _enemySpawnPoint = enemySpawnPoint;
            _finalBossBuilder = finalBossBuilder;
        }
        
        public void SpawnFinalBoss()
        {
            var spawnPositions = _enemySpawnPoint.GetEnemySpawnedPointXs()
                .Zip(_enemySpawnPoint.GetEnemySpawnedPointYs(), (x, y) => new UnityEngine.Vector3(x.x, y.y))
                .ToList();
            var middlePosition = spawnPositions[spawnPositions.Count / 2];
                
            SpawnFinalBoss(middlePosition); 
        }
        
        void SpawnFinalBoss(Vector3 spawnPosition)
        {
            var enemyMonoPrefab = _finalBossMonoLoader.Load();
            Debug.Log($"enemyMonoPrefab: {enemyMonoPrefab}, spawnPosition: {spawnPosition}");
            var enemyMonoObject = Object.Instantiate(enemyMonoPrefab, spawnPosition, Quaternion.identity);
            enemyMonoObject.Initialize(
                _container.Resolve<PlayerHolder>()
                , _container.Resolve<IEnemySpawnPoint>()
                , _container.Resolve<IFinalBossParamData>()
                , _container.Resolve<IFinalBossViewParamData>()
                ); 
            var enemyMono = _finalBossBuilder.Build(enemyMonoObject, enemyMonoObject ,EnemyEnum.FinalBoss);
            _enemyCluster.Add(enemyMono);
        }
    } 
}

