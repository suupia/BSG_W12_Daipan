#nullable enable
using System.Linq;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts.Utility;
using VContainer;
using VContainer.Unity;

namespace Daipan.Enemy.Scripts
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
            SpawnEnemy(EnemyType.A, enemyMonoPrefab);
        }


        void SpawnEnemy(EnemyType enemyType, EnemyMono enemyMonoPrefab)
        {
            var enemyObject = _container.Instantiate(enemyMonoPrefab);
            enemyObject.PureInitialize(_attributeParameters.enemyParameters.First(x => x.enemyType == enemyType));

            _enemyCluster.AddEnemy(enemyObject);
        }
    }
}