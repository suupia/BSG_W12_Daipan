#nullable enable
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Scripts.Utility.Scripts;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerAttackEffectPrefabLoaderNetwork : IPrefabLoader<PlayerAttackEffectNet>
    {
        readonly PrefabLoaderFromResources<PlayerAttackEffectNet> _loader;


        public PlayerAttackEffectPrefabLoaderNetwork()
        {
            _loader = new PrefabLoaderFromResources<PlayerAttackEffectNet>("PlayerAttackEffectNet");
        }

        public PlayerAttackEffectNet Load()
        {
            return _loader.Load();
        }
    }
}