#nullable enable
using UnityEngine;
using Daipan.Core.Interfaces;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Tower.MonoScripts;
using VContainer;
using VContainer.Unity;

namespace Daipan.Tower.Scripts
{
    public sealed class TowerSpawner : IStart
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<TowerMono> _towerMonoLoader;
        readonly TowerParamsConfig _towerParamsConfig;

        [Inject]
        public TowerSpawner(
            IObjectResolver container,
            IPrefabLoader<TowerMono> towerMonoLoader,
            TowerParamsConfig towerParamsConfig)
        {
            _container = container;
            _towerMonoLoader = towerMonoLoader;
            _towerParamsConfig = towerParamsConfig;
        }

        void IStart.Start()
        {
            var towerMonoPrefab = _towerMonoLoader.Load();
            var position = _towerParamsConfig.GetTowerSpawnPosition();
            _container.Instantiate(towerMonoPrefab, position, Quaternion.identity);
        }
    }
}