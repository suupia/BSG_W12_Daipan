#nullable enable
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Scripts.Utility.Scripts;

namespace Daipan.Player.Scripts
{
    public class PlayerAttackEffectPrefabLoader : IPrefabLoader<PlayerAttackEffectMono>
    {
        readonly PrefabLoaderFromResources<PlayerAttackEffectMono> _loader;


        public PlayerAttackEffectPrefabLoader()
        {
            _loader = new PrefabLoaderFromResources<PlayerAttackEffectMono>("PlayerAttackEffect");
        }

        public PlayerAttackEffectMono Load()
        {
            return _loader.Load();
        }
    }
}