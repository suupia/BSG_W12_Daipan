#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using DG.Tweening;

namespace Daipan.Enemy.Scripts
{
    public class EnemyDied
    {
        public event EventHandler<DiedEventArgs>? OnDied;
        readonly EnemyCluster _enemyCluster;
        readonly EnemyMono _enemyMono;
        
        public EnemyDied(EnemyCluster enemyCluster, EnemyMono enemyMono)
        {
            _enemyCluster = enemyCluster;
            _enemyMono = enemyMono;
        }

        public void Died(AbstractEnemyViewMono? enemyViewMono, bool isDaipaned = false, bool isTriggerCallback = true)
        {
            // Callback
            if (isTriggerCallback)
            {
                var args = new DiedEventArgs(_enemyMono.EnemyEnum);
                OnDied?.Invoke(_enemyMono, args);
            }

            OnDiedProcess(_enemyMono, _enemyCluster, isDaipaned, enemyViewMono);
        }

        static void OnDiedProcess(EnemyMono enemyMono, EnemyCluster enemyCluster, bool isDaipaned,
            AbstractEnemyViewMono? enemyViewMono)
        {
            if (enemyViewMono == null)
            {
                enemyCluster.Remove(enemyMono);
                return;
            }

            if (isDaipaned)
                enemyMono.transform
                    .DOMoveY(-1.7f, 0.3f)
                    .SetEase(Ease.InQuint)
                    .OnStart(() => { enemyViewMono.Daipaned(() => enemyCluster.Remove(enemyMono)); });
            else
                enemyViewMono.Died(() => enemyCluster.Remove(enemyMono));
        }
    }
}