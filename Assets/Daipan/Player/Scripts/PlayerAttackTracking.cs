#nullable enable
using System;
using System.Linq;
using Daipan.Enemy.MonoScripts;
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public class PlayerAttackTracking : IPlayerAttackMove
    {
        const double Speed = 30;
        const double HitDistance = 1.0;
        public event EventHandler<OnHitEventArgs>? OnHit;
        readonly IPlayerParamData? _playerParamData;
        readonly Func<EnemyMono?> _getNearestEnemyMono; 
        Vector3 Direction { get; set; } = Vector3.right;
    
        readonly PlayerAttackEffectMono _playerAttackEffectMono;
        public  PlayerAttackTracking(
            PlayerAttackEffectMono playerAttackEffectMono
            , IPlayerParamData playerParamData
            , Func<EnemyMono?> getTargetEnemyMono
        )
        {
            _playerAttackEffectMono = playerAttackEffectMono;
            _playerParamData = playerParamData;
            _getNearestEnemyMono = getTargetEnemyMono;
        }
        public void Move()
        {
            if (_playerAttackEffectMono == null) return;
            
            // [Precondition]
            if (_playerParamData == null)
            {
                Debug.LogWarning("PlayerAttackEffectMono: PlayerParamData is null");
                return;
            }
        
            var enemyMono = _getNearestEnemyMono();
            if (enemyMono != null && !PlayerAttackModule.IsInStreamScreen(enemyMono.transform.position))
                enemyMono = null; 
        
            Direction = enemyMono != null ? (enemyMono.transform.position - _playerAttackEffectMono.transform.position).normalized : Direction;
            _playerAttackEffectMono.transform.position += Direction * (float)(Speed * Time.deltaTime);
            if (enemyMono != null)
            {
                if (enemyMono.transform.position.x - _playerAttackEffectMono.transform.position.x < HitDistance)
                {
                    var isTargetEnemy = PlayerAttackModule.GetTargetEnemyEnum(_playerParamData.PlayerEnum())
                        .Contains(enemyMono.EnemyEnum);
                    OnHit?.Invoke(this, new OnHitEventArgs(enemyMono, isTargetEnemy));
                    if (isTargetEnemy)
                    {
                        UnityEngine.Object.Destroy(_playerAttackEffectMono.gameObject);
                    }
                    else
                    {
                        _playerAttackEffectMono.Defenced(enemyMono.transform.position);
                    }
        
                }
            }
            else
            {
                if (_playerAttackEffectMono.transform.position.x > 10) // todo: parameterからもらう
                    UnityEngine.Object.Destroy(_playerAttackEffectMono.gameObject);
            }
        }
        
 
    } 
}

