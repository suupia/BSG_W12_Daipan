using System.Collections;
using System.Collections.Generic;
using Stream.Utility;
using Stream.Utility.Scripts;
using UnityEngine;

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