#nullable enable
using System;
using System.Numerics;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using DG.Tweening;


namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyDie
    {
        public event EventHandler<DiedEventArgs>? OnDied;
        readonly AbstractEnemyMono _enemyMono;

        Vector2 _streamerPosition = new Vector2(-5.54f, -2.65f);
        float _daipanWaveSpeed = 40f;

        public EnemyDie(AbstractEnemyMono enemyMono)
        {
            _enemyMono = enemyMono;
        }
        bool IsDead { get; set; }  // Die()の処理が2回以上呼ばれるのを防ぐためのフラグ

        public void Died(EnemyViewMono? enemyViewMono)
        {
            if (IsDead) return;
            IsDead = true;
            var args = new DiedEventArgs(_enemyMono.EnemyEnum);
            OnDied?.Invoke(_enemyMono, args);
            if (enemyViewMono == null)
            {
                UnityEngine.Object.Destroy(_enemyMono.gameObject);
                return;
            }

            enemyViewMono.Died(() => UnityEngine.Object.Destroy(_enemyMono.gameObject));
        }

        public void DiedByDaipan(EnemyViewMono? enemyViewMono)
        {
            if (IsDead) return;
            IsDead = true;
            var args = new DiedEventArgs(_enemyMono.EnemyEnum);
            OnDied?.Invoke(_enemyMono, args);
            if (enemyViewMono == null)
            {
                UnityEngine.Object.Destroy(_enemyMono.gameObject);
                return;
            }

            float delayTime = (
                new Vector2(_enemyMono.transform.position.x, _enemyMono.transform.position.y) - _streamerPosition)
                .Length() / _daipanWaveSpeed;
            _enemyMono.transform
                .DOMoveY(-1.7f, 0.3f)
                .SetEase(Ease.InQuint)
                .SetDelay(delayTime)
                .OnStart(() => { enemyViewMono.Daipaned(() => UnityEngine.Object.Destroy(_enemyMono.gameObject)); });
        }

        public void DiedBySpecialBlack(EnemyViewMono? enemyViewMono)
        {
            if (IsDead) return;
            IsDead = true;
            var args = new DiedEventArgs(_enemyMono.EnemyEnum);
            OnDied?.Invoke(_enemyMono, args);
            if (enemyViewMono == null)
            {
                UnityEngine.Object.Destroy(_enemyMono.gameObject);
                return;
            }

            var enemySpecialViewMono = enemyViewMono.EnemySpecialViewMono;
            // 違う色に攻撃したのなら、特殊アニメーションを再生し、Destroy
            enemySpecialViewMono.SpecialBlack(() =>
            {
                UnityEngine.Object.Destroy(_enemyMono.gameObject);
            });
        }


    }
    public record DiedEventArgs(EnemyEnum EnemyEnum);
}