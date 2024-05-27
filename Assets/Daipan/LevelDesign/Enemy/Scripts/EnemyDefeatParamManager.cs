using System;
using UnityEngine;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    [CreateAssetMenu(fileName = "EnemyDefeatParamManager", menuName = "ScriptableObjects/Enemy/EnemyDefeatParamManager", order = 1)]
    public sealed class EnemyDefeatParamManager : ScriptableObject
    {
        [SerializeField] public EnemyQuickDefeatParam enemyQuickDefeatParam = null!;
        [SerializeField] public EnemySlowDefeatParam enemySlowDefeatParam = null!;
    }

    [Serializable]
    public sealed class EnemyQuickDefeatParam
    {
    }

    [Serializable]
    public sealed class EnemySlowDefeatParam
    {
        [Header("この数だけ敵が特定の座標を超えるとアンチコメントが生成される")] [Min(0)]
        public int slowDefeatThreshold = 5;
    }
}
