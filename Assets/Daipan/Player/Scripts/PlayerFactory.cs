using Daipan.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Player.Scripts
{
    public class PlayerFactory : IStartable
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<PlayerMono> _loader;

        [Inject]
        public PlayerFactory(
            IObjectResolver container,
            IPrefabLoader<PlayerMono> loader)
        {
            _container = container;
            _loader = loader;
        }

        void IStartable.Start()
        {
           // PlayerMonoのプレハブをロードして生成 
           var playerMonoPrefab = _loader.Load(); 
           // IObjectResolverを使ってPlayerMonoを生成すると依存関係が解決される
           var playerMono = _container.Instantiate(playerMonoPrefab);

        }

    }
}