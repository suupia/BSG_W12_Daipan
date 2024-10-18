#nullable enable
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Scripts.Utility.Scripts;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerPrefabLoaderNetwork : IPrefabLoader<PlayerNet>
    {
        readonly PrefabLoaderFromResources<PlayerNet> _loader;


        public PlayerPrefabLoaderNetwork()
        {
            _loader = new PrefabLoaderFromResources<PlayerNet>("Player");
        }

        public PlayerNet Load()
        {
            return _loader.Load();
        }
    }
}