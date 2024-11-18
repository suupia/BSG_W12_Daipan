#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Daipan.Daipan;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Transporter;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Fusion;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerSpawnerNetwork : IStart
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<PlayerNet> _playerMonoLoader;
        readonly PlayerHolder _playerHolder;
        readonly PlayerSpawnPointData _playerSpawnPointData;
        readonly TowerParamsConfig _towerParamsConfig;
        readonly IPlayerBuilder _playerBuilder;
        readonly PlayerDataTransporterNetWrapper _playerDataTransporterNetWrapper;
        readonly NetworkRunner _runner;

        [Inject]
        public PlayerSpawnerNetwork(
            IObjectResolver container
            , IPrefabLoader<PlayerNet> playerMonoLoader
            , PlayerHolder playerHolder
            , PlayerSpawnPointData playerSpawnPointData
            , TowerParamsConfig towerParamsConfig
            , IPlayerBuilder playerBuilder
            , PlayerDataTransporterNetWrapper playerDataTransporterNetWrapper
            , NetworkRunner runner
            )
        {
            _container = container;
            _playerMonoLoader = playerMonoLoader;
            _playerHolder = playerHolder;
            _playerSpawnPointData = playerSpawnPointData;
            _towerParamsConfig = towerParamsConfig;
            _playerBuilder = playerBuilder;
            _playerDataTransporterNetWrapper = playerDataTransporterNetWrapper;
            _runner = runner;
        }

        void IStart.Start()
        {
            Spawn();
            Debug.Log($"PlayerSpawnerNetwork started {this.GetHashCode()}");
        }

        async void Spawn()
        {
            if (_playerDataTransporterNetWrapper.GetPlayerRoleEnum(_runner.LocalPlayer) != PlayerRoleEnum.Streamer) return;
            var playerMonoPrefab = _playerMonoLoader.Load();
            var position = _towerParamsConfig.GetTowerSpawnPosition();
            var positionOnlyX = new Vector3(_playerSpawnPointData.GetPlayerSpawnedPointX().playerSpawnTransformX.position.x, 0, position.z);

            var playerMono = await _runner.SpawnAsync(playerMonoPrefab, positionOnlyX, Quaternion.identity);
            _playerHolder.PlayerMono = playerMono.GetComponent<IPlayerMono>();
        }

    }
}