#nullable enable
using Daipan.Core.Interfaces;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerSpawner : IStart
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<PlayerMono> _playerMonoLoader;
        readonly PlayerHolder _playerHolder;
        readonly PlayerSpawnPointData _playerSpawnPointData;
        readonly TowerParamsConfig _towerParamsConfig;

        [Inject]
        public PlayerSpawner(
            IObjectResolver container,
            IPrefabLoader<PlayerMono> playerMonoLoader,
            PlayerHolder playerHolder,
            PlayerSpawnPointData playerSpawnPointData,
            TowerParamsConfig towerParamsConfig)
        {
            _container = container;
            _playerMonoLoader = playerMonoLoader;
            _playerHolder = playerHolder;
            _playerSpawnPointData = playerSpawnPointData;
            _towerParamsConfig = towerParamsConfig;
        }

        void IStart.Start()
        {
            var playerMonoPrefab = _playerMonoLoader.Load();
            var position = _towerParamsConfig.GetTowerSpawnPosition();
            var positionOnlyX = new Vector3(_playerSpawnPointData.GetPlayerSpawnedPointX().playerSpawnTransformX.position.x, 0, position.z); 
            var playerMono = _container.Instantiate(playerMonoPrefab, positionOnlyX, Quaternion.identity); 
            _playerHolder.PlayerMono = playerMono;
        }
    }
}