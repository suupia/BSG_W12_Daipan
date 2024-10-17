#nullable enable
using Daipan.Enemy.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Scripts.Utility.Scripts;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyNetPrefabLoader : IPrefabLoader<EnemyNet>
    {
        readonly PrefabLoaderFromResources<EnemyNet> _loader;

        public EnemyNetPrefabLoader()
        {
            _loader = new PrefabLoaderFromResources<EnemyNet>("EnemyNet");
        }

        public EnemyNet Load()
        {
            return _loader.Load();
        }
    }
}