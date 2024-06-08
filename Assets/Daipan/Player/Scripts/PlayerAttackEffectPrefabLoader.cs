#nullable enable
using Daipan.Stream.Scripts.Utility.Scripts;

namespace Daipan.Player.Scripts
{
    public class PlayerAttackEffectPrefabLoader
    {
        readonly PrefabLoaderFromResources<PlayerMono> _loader;


        public PlayerAttackEffectPrefabLoader()
        {
            _loader = new PrefabLoaderFromResources<PlayerMono>("PlayerAttackEffect");
        }

        public PlayerMono Load()
        {
            return _loader.Load();
        }
    }
}