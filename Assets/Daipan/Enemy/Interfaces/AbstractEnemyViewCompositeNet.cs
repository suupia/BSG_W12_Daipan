#nullable enable
using System;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Fusion;
using UnityEngine;

namespace Daipan.Enemy.Interfaces
{
    // SerializeFieldで受け取れるように抽象クラスとした
    public abstract class AbstractEnemyViewCompositeNet :
        NetworkBehaviour,
        IEnemyViewMono,
        IEnemyViewMonoSetDomain,
        IEnemyViewEndCallbacksFacade
    {
        public abstract void SetDomain(IEnemyViewParamData enemyParamData);
        public abstract void SetOnDiedCallback(Action onDied);
        public abstract void SetOnDaipanedCallback(Action onDaipaned);
        public abstract void SetSpecialBlackCallback(Action onSpecialBlack);
        public abstract void SetHpGauge(double currentHp, int maxHp);
        public abstract void Move();
        public abstract void Attack();
        public abstract void Died();
        public abstract void Daipaned();
        public abstract void Highlight(bool isHighlighted);
    }
}