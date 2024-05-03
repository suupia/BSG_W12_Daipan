using Stream.Utility;
using VContainer;
using VContainer.Unity;

namespace Stream.Player.Scripts
{
    public sealed class PlayerFactory : IStartable
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<PlayerMono> _playerMonoLoader;

        [Inject]
        public PlayerFactory(
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
            var playerMono = _container.Instantiate(playerMonoPrefab);
        }
    }
}