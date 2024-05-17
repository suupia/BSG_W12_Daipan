#nullable enable
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Scripts.Utility.Scripts;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerPrefabLoader : IPrefabLoader<PlayerMono>
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