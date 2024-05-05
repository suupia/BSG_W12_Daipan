using System.Collections;
using System.Collections.Generic;
using Stream.Utility;
using UnityEngine;

namespace Enemy
{
    public class EnemyFactory
    {
        readonly IPrefabLoader<EnemyMono> _enemyMonoloader;
    }
}