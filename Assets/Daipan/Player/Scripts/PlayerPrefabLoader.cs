#nullable enable
using Stream.Utility;
using Stream.Utility.Scripts;

namespace Stream.Player.Scripts
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