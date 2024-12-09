#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using DG.Tweening;
using UnityEngine;


namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyDie
    {
        public event EventHandler<DiedEventArgs>? OnDied;
        readonly IEnemyMono _enemyMono;
        readonly Vector2 _streamerPosition = new Vector2(-5.54f, -2.65f);
        readonly float _daipanWaveSpeed = 40f;

        public EnemyDie(IEnemyMono enemyMono)
        {
            _enemyMono = enemyMono;
        }
        bool IsDead { get; set; }  // Die()の処理が2回以上呼ばれるのを防ぐためのフラグ

        public void Died(IEnemyViewMono? enemyViewMono)
        {
            if (IsDead) return;
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

        public void DiedByDaipan(IEnemyViewMono? enemyViewMono)
        {
            if (IsDead) return;
            IsDead = true;
            var args = new DiedEventArgs(_enemyMono.EnemyEnum);
            OnDied?.Invoke(_enemyMono, args);
            if (enemyViewMono == null)
            {
                UnityEngine.Object.Destroy(_enemyMono.GameObject);
                return;
            }

            float delayTime = (new Vector2(_enemyMono.Transform.position.x, _enemyMono.Transform.position.y) - _streamerPosition)
                .magnitude / _daipanWaveSpeed;
            _enemyMono.Transform
                .DOMoveY(-1.7f, 0.3f)
                .SetEase(Ease.InQuint)
                .SetDelay(delayTime)
                .OnStart(() => { enemyViewMono.Daipaned(() => UnityEngine.Object.Destroy(_enemyMono.GameObject)); });
        }
        

        public void DiedBySpecialBlack(IEnemyViewMono? enemyViewMono)
        {
            if (IsDead) return;
            IsDead = true;
            var args = new DiedEventArgs(_enemyMono.EnemyEnum);
            OnDied?.Invoke(_enemyMono, args);
            if (enemyViewMono == null)
            {
                UnityEngine.Object.Destroy(_enemyMono.GameObject);
                return;
            }

            if(enemyViewMono is IEnemySpecialView specialEnemyViewMono)
            {
                // 違う色に攻撃したのなら、特殊アニメーションを再生し、Destroy
                specialEnemyViewMono.SpecialBlack(() => UnityEngine.Object.Destroy(_enemyMono.GameObject));
            }
            else
            {
                // クライアントでSpecialかどうかを判定しているので、警告は出ないはず
                Debug.LogWarning("Special enemy die but not special enemy view mono");
            }
        }


    }
    public record DiedEventArgs(EnemyEnum EnemyEnum);
}