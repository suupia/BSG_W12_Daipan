#nullable enable
using UnityEngine;

namespace Daipan.Enemy.Interfaces
{
    public abstract class AbstractEnemyMono : MonoBehaviour, IEnemyMonoDie, IHighlightable
    {
        public abstract void Die(AbstractEnemyMono enemyMono, bool isDaipaned = false);

        public abstract void Highlight(bool isHighlighted);
    }
}