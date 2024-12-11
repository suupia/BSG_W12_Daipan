#nullable enable
using System;
using System.Runtime.CompilerServices;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyViewMono
    {
        public IEnemyViewMono GetEnemyViewMono => this; 
        public void SetHpGauge(double currentHp, int maxHp);
        public void Move();
        public void Attack();
        public void Highlight(bool isHighlighted);
    }
}