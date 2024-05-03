#nullable enable
using Daipan.Utility;
using Daipan.Utility.Scripts;
using VContainer;

namespace Daipan.Player.Scripts
{
    public class PlayerPrefabLoader : IPrefabLoader<PlayerMono>
    {
        readonly PrefabLoaderFromResources<PlayerMono> _loader;
     

        public PlayerPrefabLoader()
        {
            _loader = new PrefabLoaderFromResources<PlayerMono>("Player");
        }
        
        public PlayerMono Load()
        {
            return _loader.Load();
        }
    }
}