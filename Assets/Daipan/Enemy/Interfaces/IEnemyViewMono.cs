#nullable enable
using System;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyViewMono
    {
        void SetHpGauge(double currentHp, int maxHp);
        void Move();
        void Attack();
        void Died(Action onDied);
        void Daipaned(Action onDaipaned);
        void Highlight(bool isHighlighted);
    }
}