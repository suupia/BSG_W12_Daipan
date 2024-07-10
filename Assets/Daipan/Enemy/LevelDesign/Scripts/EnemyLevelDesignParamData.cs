#nullable enable
using System;
using Daipan.Enemy.LevelDesign.Interfaces;

namespace Daipan.Enemy.LevelDesign.Scripts
{
    public sealed class EnemyLevelDesignParamData : IEnemyLevelDesignParamData
    {
        readonly EnemyLevelDesignParam _enemyLevelDesignParam;
        public EnemyLevelDesignParamData(
            EnemyLevelDesignParam levelDesignParam
        )
        {
            _enemyLevelDesignParam = levelDesignParam;
        }
        public int GetIncreaseViewerOnEnemyKill() => _enemyLevelDesignParam.increaseViewerOnEnemyKill;
        public int GetIncreaseIrritationGaugeOnSpecialEnemyKill() => _enemyLevelDesignParam.increaseIrritationGaugeOnSpecialEnemyKill;
        
        public int CurrentKillAmount
        {
            get => _enemyLevelDesignParam.currentKillAmount;
            set => _enemyLevelDesignParam.currentKillAmount = value;
        }
    }
}