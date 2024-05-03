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
            Debug.Log($"PlayerFactory Constructor: {loader}");
        }

        void IStartable.Start()
        {
           var playerMonoPrefab = _loader.Load(); 
           var playerMono = _container.Instantiate(playerMonoPrefab);

        }

    }
}