#nullable enable
using System;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Interfaces
{
    // SerializeFieldで受け取れるように抽象クラスとした
    public abstract class AbstractEnemyViewMono : MonoBehaviour
    {
        public abstract void SetDomain(IEnemyParamData enemyParamData);
        public abstract void SetHpGauge(int currentHp, int maxHp);
        public abstract void Move();
        public abstract void Attack();
        public abstract void Died(Action onDied);
        public abstract void Daipaned(Action onDaipaned);

    }
}