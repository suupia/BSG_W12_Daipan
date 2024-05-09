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

        [Inject]
        public EnemySpawner(
            IObjectResolver container,
            IPrefabLoader<EnemyMono> enemyMonoLoader)
        {
            _container = container;
            _enemyMonoLoader = enemyMonoLoader;
        }

        void IStartable.Start()
        {
            var enemyMonoPrefab = _enemyMonoLoader.Load();
            var playerMono = _container.Instantiate(enemyMonoPrefab);
        }
    }
}