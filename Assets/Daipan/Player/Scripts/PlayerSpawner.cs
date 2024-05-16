#nullable enable
using Daipan.Core.Interfaces;
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

        [Inject]
        public PlayerSpawner(
            IObjectResolver container,
            IPrefabLoader<PlayerMono> playerMonoLoader,
            PlayerHolder playerHolder)
        {
            _container = container;
            _playerMonoLoader = playerMonoLoader;
            _playerHolder = playerHolder;
        }

        void IStart.Start()
        {
            // PlayerMonoのプレハブをロードして生成 
            var playerMonoPrefab = _playerMonoLoader.Load();
            // IObjectResolverを使ってPlayerMonoを生成すると依存関係が解決される
            var position = new Vector3(-8, 0, 0); // 左
            var playerMono = _container.Instantiate(playerMonoPrefab, position, Quaternion.identity);
            _playerHolder.PlayerMono = playerMono;
        }
    }
}