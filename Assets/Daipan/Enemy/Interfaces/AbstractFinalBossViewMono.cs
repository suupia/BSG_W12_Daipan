#nullable enable
using System;
using UnityEngine;

namespace Daipan.Enemy.Interfaces
{
    public abstract class AbstractFinalBossViewMono : MonoBehaviour, IFinalBossViewMono , IFinalBossViewMonoSetDomain
    {
        public abstract void SetDomain(IFinalBossViewParamData finalBossViewParamData);
        public abstract void SetHpGauge(double currentHp, int maxHp);
        public abstract void Move();
        public abstract void Attack();
        public abstract void Died(Action onDied);
        public abstract void Daipaned(Action onDaipaned);
        public abstract void DaipanHit();
        public abstract void Highlight(bool isHighlighted);
        
        // FinalBoss
        public abstract void SummonEnemy();
    } 
}

