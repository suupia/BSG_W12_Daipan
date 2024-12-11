#nullable enable
using System;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Interfaces
{
    // SerializeFieldで受け取れるように抽象クラスとした
    // ViewのMonoBehaviourの基底クラスとして使用していて、そのViewのMonoBehaviourの外では一切使用していない
    public abstract class AbstractEnemyViewCompositeMono : MonoBehaviour, IEnemyViewMono ,IEnemyViewEndCallbacksFacade , IEnemyViewMonoSetDomain 
    {
        public abstract void SetDomain(IEnemyViewParamData enemyParamData);
        public abstract void SetOnDiedCallback(Action onDied);
        public abstract void SetOnDaipanedCallback(Action onDaipaned);
        public abstract void SetHpGauge(double currentHp, int maxHp);
        public abstract void Move();
        public abstract void Attack();
        public abstract void Died();
        public abstract void Daipaned();
        public abstract void Highlight(bool isHighlighted);

    }
}