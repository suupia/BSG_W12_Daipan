#nullable enable
using System;

namespace Daipan.Enemy.Interfaces
{
    public interface IFinalBossViewMono : IEnemyViewMono
    {
        public void SummonEnemy();
        public void DaipanHit();
    }
}