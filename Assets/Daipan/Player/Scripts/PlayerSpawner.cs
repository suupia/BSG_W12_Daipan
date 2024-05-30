#nullable enable
using Daipan.Core.Interfaces;
using Daipan.LevelDesign.Enemy.Scripts;
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
        readonly TowerParamsConfig _towerParamsConfig;

        [Inject]
        public PlayerSpawner(
            IObjectResolver container,
            IPrefabLoader<PlayerMono> playerMonoLoader,
            PlayerHolder playerHolder,
            TowerParamsConfig towerParamsConfig)
        {
            _container = container;
            _playerMonoLoader = playerMonoLoader;
            _playerHolder = playerHolder;
            _towerParamsConfig = towerParamsConfig;
        }

        void IStart.Start()
        {
            // PlayerMonoのプレハブをロードして生成 
            var playerMonoPrefab = _playerMonoLoader.Load();
            // IObjectResolverを使ってPlayerMonoを生成すると依存関係が解決される
            var position = _towerParamsConfig.GetTowerSpawnPosition();
            var playerMono = _container.Instantiate(playerMonoPrefab, position, Quaternion.identity);
            _playerHolder.PlayerMono = playerMono;
        }
    }
}