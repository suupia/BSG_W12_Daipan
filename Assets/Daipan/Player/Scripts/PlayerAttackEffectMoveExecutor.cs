#nullable enable
using System;
using System.Linq;
using Daipan.Enemy.MonoScripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    // public class PlayerAttackEffectMoveExecutor
    // {
    //     readonly double _speed = 30;
    //     readonly double _hitDistance = 1.0;
    //     public event EventHandler<OnHitEventArgs>? OnHit;
    //     IPlayerParamData? _playerParamData;
    //     Func<EnemyMono?> _getNearestEnemyMono = () => null;
    //     Vector3 Direction { get; set; } = Vector3.right;
    //
    //     PlayerAttackEffectMono? _playerAttackEffectMono;
    //     public void Move()
    //     {
    //         if (_playerAttackEffectMono == null) return;
    //         
    //         // [Precondition]
    //         if (_playerParamData == null)
    //         {
    //             Debug.LogWarning("PlayerAttackEffectMono: PlayerParamData is null");
    //             return;
    //         }
    //     
    //         var enemyMono = _getNearestEnemyMono();
    //         enemyMono = PlayerAttackModule.IsInScreenEnemy(enemyMono) ? enemyMono : null;
    //     
    //         Direction = enemyMono != null ? (enemyMono.transform.position - _playerAttackEffectMono.transform.position).normalized : Direction;
    //         _playerAttackEffectMono.transform.position += Direction * (float)(_speed * Time.deltaTime);
    //         if (enemyMono != null)
    //         {
    //             if (enemyMono.transform.position.x - _playerAttackEffectMono.transform.position.x < _hitDistance)
    //             {
    //                 var isTargetEnemy = PlayerAttackModule.GetTargetEnemyEnum(_playerParamData.PlayerEnum())
    //                     .Contains(enemyMono.EnemyEnum);
    //                 OnHit?.Invoke(this, new OnHitEventArgs(enemyMono, isTargetEnemy));
    //                 UnityEngine.Object.Destroy(_playerAttackEffectMono.gameObject);
    //     
    //             }
    //         }
    //         else
    //         {
    //             if (_playerAttackEffectMono.transform.position.x > 10) // todo: parameterからもらう
    //                 UnityEngine.Object.Destroy(_playerAttackEffectMono.gameObject);
    //         }
    //     }
    //     
    //     public void SetDomain(
    //         PlayerAttackEffectMono playerAttackEffectMono
    //         , IPlayerParamData playerParamData
    //         , Func<EnemyMono?> getTargetEnemyMono
    //         )
    //     {
    //         _playerAttackEffectMono = playerAttackEffectMono;
    //         _playerParamData = playerParamData;
    //         _getNearestEnemyMono = getTargetEnemyMono;
    //     }
    // } 
}
