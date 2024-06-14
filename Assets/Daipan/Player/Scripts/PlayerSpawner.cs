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
            var positionOnlyX = new Vector3(position.x + 1.0f, 0, position.z); // todo : 一旦ポジションをタワーに合わせるのはやめる
            var playerMono = _container.Instantiate(playerMonoPrefab, positionOnlyX, Quaternion.identity); 
            _playerHolder.PlayerMono = playerMono;
        }
    }
}