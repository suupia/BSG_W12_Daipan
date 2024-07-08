#nullable enable
using System;
using System.Linq;
using Daipan.Enemy.MonoScripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public class PlayerAttackEffectMoveExecutor
    {
        // readonly double _speed = 30;
        // readonly double _hitDistance = 1.0;
        // public event EventHandler<OnHitEventArgs>? OnHit;
        // IPlayerParamData? _playerParamData;
        // Func<EnemyMono?> _getNearestEnemyMono = () => null;
        //
        //
        // public void Move()
        // {
        //     // [Precondition]
        //     if (_playerParamData == null)
        //     {
        //         Debug.LogWarning("PlayerAttackEffectMono: PlayerParamData is null");
        //         return;
        //     }
        //
        //     var enemyMono = _getNearestEnemyMono();
        //
        //     if(enemyMono != null)
        //     {
        //         if (!PlayerAttackModule.IsInScreenEnemy(enemyMono))
        //         {
        //             enemyMono = null;
        //         }
        //         
        //     }
        //
        //     Direction = enemyMono != null ? (enemyMono.transform.position - transform.position).normalized : Direction;
        //     transform.position += Direction * (float)(_speed * Time.deltaTime);
        //     if (enemyMono != null)
        //     {
        //         if (enemyMono.transform.position.x - transform.position.x < _hitDistance)
        //         {
        //             var isTargetEnemy = PlayerAttackModule.GetTargetEnemyEnum(_playerParamData.PlayerEnum())
        //                 .Contains(enemyMono.EnemyEnum);
        //             OnHit?.Invoke(this, new OnHitEventArgs(enemyMono, isTargetEnemy));
        //             Destroy(gameObject);
        //
        //         }
        //     }
        //     else
        //     {
        //         if (transform.position.x > 10) // todo: parameterからもらう
        //             Destroy(gameObject);
        //     }
        // }
    } 
}

