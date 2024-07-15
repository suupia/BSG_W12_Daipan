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
        readonly AbstractEnemyMono _enemyMono;
        
        public EnemyDie(AbstractEnemyMono enemyMono)
        {
            _enemyMono = enemyMono;
        }

        public void Died(AbstractEnemyViewMono? enemyViewMono, bool isDaipaned = false)
        {
            var args = new DiedEventArgs(_enemyMono.EnemyEnum);
            OnDied?.Invoke(_enemyMono, args);

            OnDiedProcess(_enemyMono, isDaipaned, enemyViewMono);
        }

        public void Died(AbstractEnemyViewMono? enemyViewMono)
        {
            var args = new DiedEventArgs(_enemyMono.EnemyEnum);
            OnDied?.Invoke(_enemyMono, args);

            if (enemyViewMono == null)
            {
                UnityEngine.Object.Destroy(_enemyMono.gameObject);
                return;
            }

            enemyViewMono.Died(() => UnityEngine.Object.Destroy(_enemyMono.gameObject));
        }

        public void DiedByDaipan(AbstractEnemyViewMono? enemyViewMono)
        {
            var args = new DiedEventArgs(_enemyMono.EnemyEnum);
            OnDied?.Invoke(_enemyMono, args);
            if (enemyViewMono == null)
            {
                UnityEngine.Object.Destroy(_enemyMono.gameObject);
                return;
            }

            _enemyMono.transform
                .DOMoveY(-1.7f, 0.3f)
                .SetEase(Ease.InQuint)
                .OnStart(() => { enemyViewMono.Daipaned(() => UnityEngine.Object.Destroy(_enemyMono.gameObject)); });
        }
        
        public void DiedBySpecialBlack(AbstractEnemyViewMono? enemyViewMono)
        {
            var args = new DiedEventArgs(_enemyMono.EnemyEnum);
            OnDied?.Invoke(_enemyMono, args);
            if (enemyViewMono == null)
            {
                UnityEngine.Object.Destroy(_enemyMono.gameObject);
                return;
            }

            var _enemySpecialViewMono = enemyViewMono as EnemySpecialViewMono; // どうにかして持ってくる
            // 違う色に攻撃したのなら、特殊アニメーションを再生し、Destroy
            _enemySpecialViewMono?.SpecialBlack(() =>
            {
                UnityEngine.Object.Destroy(_enemyMono.gameObject);
            });
        }

        static void OnDiedProcess(
            AbstractEnemyMono enemyMono, 
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
    public record DiedEventArgs(EnemyEnum EnemyEnum);
}