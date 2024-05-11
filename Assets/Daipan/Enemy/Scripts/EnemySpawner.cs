#nullable enable
using System.Linq;
using Stream.Utility;
using VContainer;
using VContainer.Unity;

namespace Enemy
{
    public class EnemySpawner : IStartable
    {
        readonly EnemyAttributeParameters _attributeParameters;
        readonly IObjectResolver _container;
        readonly EnemyCluster _enemyCluster;
        readonly IPrefabLoader<EnemyMono> _enemyMonoLoader;

        EnemyMono _enemyMonoPrefab;

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
            _enemyMonoPrefab = _enemyMonoLoader.Load();
            //Debug.Log(string.Join("\n", _attributeParameters.enemyParameters));
            SpawnEnemy(ENEMY_TYPE.A_Type);
        }


        void SpawnEnemy(ENEMY_TYPE enemyType)
        {
            var enemyObject = _container.Instantiate(_enemyMonoPrefab);
            enemyObject.PureInitialize(_attributeParameters.enemyParameters.First(x => x.enemyType == enemyType));

            EnemyCluster.Instance.AddEnemy(enemyObject);
        }
    }
}