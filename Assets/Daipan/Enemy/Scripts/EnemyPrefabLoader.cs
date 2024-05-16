#nullable enable
using Daipan.Enemy.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Scripts.Utility.Scripts;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyPrefabLoader : IPrefabLoader<EnemyMono>
    {
        readonly PrefabLoaderFromResources<EnemyMono> _loader;

        public EnemyPrefabLoader()
        {
            _loader = new PrefabLoaderFromResources<EnemyMono>("Enemy");
        }

        public EnemyMono Load()
        {
            return _loader.Load();
        }
    }
}