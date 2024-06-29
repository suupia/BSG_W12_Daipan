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
        readonly EnemyMono _enemyMono;
        
        public EnemyDied(EnemyMono enemyMono)
        {
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

            OnDiedProcess(_enemyMono, isDaipaned, enemyViewMono);
        }

        static void OnDiedProcess(
            EnemyMono enemyMono, 
            bool isDaipaned,
            AbstractEnemyViewMono? enemyViewMono
            )
        {
            if (enemyViewMono == null)
            {
                UnityEngine.Object.Destroy(enemyMono.gameObject); 
                return;
            }

            if (isDaipaned)
                enemyMono.transform
                    .DOMoveY(-1.7f, 0.3f)
                    .SetEase(Ease.InQuint)
                    .OnStart(() => { enemyViewMono.Daipaned(() =>  UnityEngine.Object.Destroy(enemyMono.gameObject)); });
            else
                enemyViewMono.Died(() =>  UnityEngine.Object.Destroy(enemyMono.gameObject));
        }
    }
}