#nullable enable
using Daipan.Enemy.Scripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Interfaces
{
    public abstract class AbstractEnemyMono : MonoBehaviour, IEnemyMonoDie, IHighlightable
    {
        public abstract EnemyEnum EnemyEnum { get; protected set; }
        public abstract Hp Hp { get; protected set; }
        public abstract bool IsReachedPlayer { get; protected set; }
        public abstract void OnAttacked(IPlayerParamData playerParamData);
        public abstract void Die(AbstractEnemyMono enemyMono, bool isDaipaned = false);

        public abstract void Highlight(bool isHighlighted);
    }
    
}