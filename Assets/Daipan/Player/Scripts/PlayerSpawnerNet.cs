#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerSpawnerNet : IStart 
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<PlayerNet> _playerMonoLoader;
        readonly PlayerHolder _playerHolder;
        readonly PlayerSpawnPointData _playerSpawnPointData;
        readonly TowerParamsConfig _towerParamsConfig;
        readonly IPlayerBuilder _playerBuilder;

        [Inject]
        public PlayerSpawnerNet(
            IObjectResolver container
            ,IPrefabLoader<PlayerNet> playerMonoLoader
            ,PlayerHolder playerHolder
            ,PlayerSpawnPointData playerSpawnPointData
            ,TowerParamsConfig towerParamsConfig
            , IPlayerBuilder playerBuilder
            )
        {
            _container = container;
            _playerMonoLoader = playerMonoLoader;
            _playerHolder = playerHolder;
            _playerSpawnPointData = playerSpawnPointData;
            _towerParamsConfig = towerParamsConfig;
            _playerBuilder = playerBuilder;
        }

        void IStart.Start()
        {
            var playerMonoPrefab = _playerMonoLoader.Load();
            var position = _towerParamsConfig.GetTowerSpawnPosition();
            var positionOnlyX = new Vector3(_playerSpawnPointData.GetPlayerSpawnedPointX().playerSpawnTransformX.position.x, 0, position.z); 
            var playerMono = _container.Instantiate(playerMonoPrefab, positionOnlyX, Quaternion.identity);
            Debug.LogWarning($"PlayerMono.Hp: {playerMono.Hp.Value}");
            var buildedPlayer = _playerBuilder.Build(playerMono);
            _playerHolder.PlayerMono = buildedPlayer;
        }
        
    }
}