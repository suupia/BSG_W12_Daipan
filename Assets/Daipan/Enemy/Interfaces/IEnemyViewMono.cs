#nullable enable
using System;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyViewMono
    {
        public void SetHpGauge(double currentHp, int maxHp);
        public void Move();
        public void Attack();
        public void Died(Action onDied);
        public void Daipaned(Action onDaipaned);
        public void Highlight(bool isHighlighted);
    }
}