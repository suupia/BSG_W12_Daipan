#nullable enable
using Stream.Utility;
using VContainer;
using VContainer.Unity;


namespace Enemy
{
    public class EnemySpawner : IStartable
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<EnemyMono> _enemyMonoLoader;
        readonly EnemyAttributeParameters _attributeParameters;

        [Inject]
        public EnemySpawner(
            IObjectResolver container,
            IPrefabLoader<EnemyMono> enemyMonoLoader,
            EnemyAttributeParameters attributeParameters)
        {
            _container = container;
            _enemyMonoLoader = enemyMonoLoader;
            _attributeParameters = attributeParameters;

        }

        void IStartable.Start()
        {
            var enemyMonoPrefab = _enemyMonoLoader.Load();
        }

    }
}