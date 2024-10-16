#nullable enable
using System;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Interfaces
{
    // SerializeFieldで受け取れるように抽象クラスとした
    public abstract class AbstractEnemyViewMono : MonoBehaviour, IEnemyViewMono , IEnemyViewMonoSetDomain 
    {
        public abstract void SetDomain(IEnemyViewParamData enemyParamData);
        public abstract void SetHpGauge(double currentHp, int maxHp);
        public abstract void Move();
        public abstract void Attack();
        public abstract void Died(Action onDied);
        public abstract void Daipaned(Action onDaipaned);
        public abstract void Highlight(bool isHighlighted);

    }
}