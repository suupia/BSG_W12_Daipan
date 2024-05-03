using Daipan.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Player.Scripts
{
    public class PlayerFactory : IStartable
    {
        readonly IPrefabLoader<PlayerMono> _loader;

        [Inject]
        public PlayerFactory(IPrefabLoader<PlayerMono> loader)
        {
            _loader = loader;
            Debug.Log($"PlayerFactory Constructor: {loader}");
        }

        void IStartable.Start()
        {
           var playerMono = Object.Instantiate(_loader.Load());
           playerMono.Initialize();
           Debug.Log($"PlayerFactory Start: {playerMono}");
           
        }

    }
}