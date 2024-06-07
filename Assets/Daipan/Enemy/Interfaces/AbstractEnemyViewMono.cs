#nullable enable
using System;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Interfaces
{
    public abstract class AbstractEnemyViewMono : MonoBehaviour
    {
        public abstract void SetDomain(EnemyParamDataContainer enemyParamDataContainer);
        public abstract void SetView(NewEnemyType enemyEnum);
        public abstract void SetHpGauge(int currentHp, int maxHp);
        public abstract void Move();
        public abstract void Attack();
        public abstract void Died(Action onDied);
        public abstract void Daipaned(Action onDaipaned);

    }
}