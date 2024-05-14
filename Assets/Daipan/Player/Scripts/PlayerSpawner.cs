using Daipan.Stream.Scripts.Utility;
using VContainer;
using VContainer.Unity;

namespace Daipan.Stream.Scripts
{
    public sealed class PlayerSpawner : IStartable
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<PlayerMono> _playerMonoLoader;

        [Inject]
        public PlayerSpawner(
            IObjectResolver container,
            IPrefabLoader<PlayerMono> playerMonoLoader)
        {
            _container = container;
            _playerMonoLoader = playerMonoLoader;
        }

        void IStartable.Start()
        {
            // PlayerMonoのプレハブをロードして生成 
            var playerMonoPrefab = _playerMonoLoader.Load();
            // IObjectResolverを使ってPlayerMonoを生成すると依存関係が解決される
            var position = new UnityEngine.Vector3(-10, 0, 0); // 左
            var playerMono = _container.Instantiate(playerMonoPrefab,position, UnityEngine.Quaternion.identity);
        }
    }
}