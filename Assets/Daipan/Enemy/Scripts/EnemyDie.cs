#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using DG.Tweening;


namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyDie
    {
        public event EventHandler<DiedEventArgs>? OnDied;
        readonly IEnemyMono _enemyMono;
        
        public EnemyDie(IEnemyMono enemyMono)
        {
            _enemyMono = enemyMono;
        }
        bool IsDead { get; set; }  // Die()の処理が2回以上呼ばれるのを防ぐためのフラグ

        public void Died(EnemyViewMono? enemyViewMono)
        {
            if(IsDead)return;
            IsDead = true;
            var args = new DiedEventArgs(_enemyMono.EnemyEnum);
            OnDied?.Invoke(_enemyMono, args);        
            if (enemyViewMono == null)
            {
                UnityEngine.Object.Destroy(_enemyMono.GameObject);
                return;
            }

            enemyViewMono.Died(() => UnityEngine.Object.Destroy(_enemyMono.GameObject));
        }

        public void DiedByDaipan(EnemyViewMono? enemyViewMono)
        {
            if(IsDead)return;
            IsDead = true;
            var args = new DiedEventArgs(_enemyMono.EnemyEnum);
            OnDied?.Invoke(_enemyMono, args);
            if (enemyViewMono == null)
            {
                UnityEngine.Object.Destroy(_enemyMono.GameObject);
                return;
            }

            _enemyMono.Transform
                .DOMoveY(-1.7f, 0.3f)
                .SetEase(Ease.InQuint)
                .OnStart(() => { enemyViewMono.Daipaned(() => UnityEngine.Object.Destroy(_enemyMono.GameObject)); });
        }
        
        public void DiedBySpecialBlack(EnemyViewMono? enemyViewMono)
        {
            if(IsDead)return;
            IsDead = true;
            var args = new DiedEventArgs(_enemyMono.EnemyEnum);
            OnDied?.Invoke(_enemyMono, args);
            if (enemyViewMono == null)
            {
                UnityEngine.Object.Destroy(_enemyMono.GameObject);
                return;
            }

            var abstractEnemyViewMono = enemyViewMono.GetAbstractEnemyViewMono();
            if(abstractEnemyViewMono is EnemySpecialViewMono specialEnemyViewMono)
            {
                // 違う色に攻撃したのなら、特殊アニメーションを再生し、Destroy
                specialEnemyViewMono.SpecialBlack(() => UnityEngine.Object.Destroy(_enemyMono.GameObject));
            }
        }


    }
    public record DiedEventArgs(EnemyEnum EnemyEnum);
}