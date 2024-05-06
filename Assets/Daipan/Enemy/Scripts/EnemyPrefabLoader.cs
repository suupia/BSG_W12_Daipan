#nullable enable
using Stream.Utility;
using Stream.Utility.Scripts;

namespace Enemy
{
    public sealed class EnemyPrefabLoader : IPrefabLoader<EnemyMono>
    {
        private readonly PrefabLoaderFromResources<EnemyMono> _loader;

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