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

        [Inject]
        public EnemySpawner(
            IObjectResolver container,
            IPrefabLoader<EnemyMono> enemyMonoLoader,
            EnemyAttributeParameters attributeParameters,
            EnemyCluster enemyCluster)
        {
            _container = container;
            _enemyMonoLoader = enemyMonoLoader;
            _attributeParameters = attributeParameters;
            _enemyCluster = enemyCluster;
        }

        void IStartable.Start()
        {
            var enemyMonoPrefab = _enemyMonoLoader.Load();
            //Debug.Log(string.Join("\n", _attributeParameters.enemyParameters));
            SpawnEnemy(ENEMY_TYPE.A_Type, enemyMonoPrefab);
        }


        void SpawnEnemy(ENEMY_TYPE enemyType, EnemyMono enemyMonoPrefab)
        {
            var enemyObject = _container.Instantiate(enemyMonoPrefab);
            enemyObject.PureInitialize(_attributeParameters.enemyParameters.First(x => x.enemyType == enemyType));

            _enemyCluster.AddEnemy(enemyObject);
        }
    }
}