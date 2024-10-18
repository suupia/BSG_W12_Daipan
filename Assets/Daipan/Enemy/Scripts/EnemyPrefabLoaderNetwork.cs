#nullable enable
using Daipan.Enemy.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Stream.Scripts.Utility.Scripts;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyPrefabLoaderNetwork : IPrefabLoader<EnemyNet>
    {
        readonly PrefabLoaderFromResources<EnemyNet> _loader;

        public EnemyPrefabLoaderNetwork()
        {
            _loader = new PrefabLoaderFromResources<EnemyNet>("EnemyNet");
        }

        public EnemyNet Load()
        {
            return _loader.Load();
        }
    }
}