#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using DG.Tweening;


namespace Daipan.Enemy.Scripts
{
    public sealed class FinalBossDie 
    {
        public event EventHandler<DiedEventArgs>? OnDied;
        readonly IEnemyMono _enemyMono;
        
        public FinalBossDie(IEnemyMono enemyMono)
        {
            _enemyMono = enemyMono;
        }

        public void Died(AbstractFinalBossViewMono? enemyViewMono, bool isDaipaned = false, bool isTriggerCallback = true)
        {
            // Callback
            if (isTriggerCallback)
            {
                var args = new DiedEventArgs(_enemyMono.EnemyEnum);
                OnDied?.Invoke(_enemyMono, args);
            }

            OnDiedProcess(_enemyMono, isDaipaned, enemyViewMono);
        }

        static void OnDiedProcess(
            IEnemyMono enemyMono, 
            bool isDaipaned,
            AbstractFinalBossViewMono? enemyViewMono
            )
        {
            if (enemyViewMono == null)
            {
                UnityEngine.Object.Destroy(enemyMono.GameObject); 
                return;
            }

            if (isDaipaned)
                enemyMono.Transform
                    .DOMoveY(-1.7f, 0.3f)
                    .SetEase(Ease.InQuint)
                    .OnStart(() => { enemyViewMono.Daipaned(() =>  UnityEngine.Object.Destroy(enemyMono.GameObject)); });
            else
                enemyViewMono.Died(() =>  UnityEngine.Object.Destroy(enemyMono.GameObject));
        }
    }
}