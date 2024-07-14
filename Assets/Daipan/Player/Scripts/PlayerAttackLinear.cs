#nullable enable
using System;
using System.Linq;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public class PlayerAttackLinear : IPlayerAttackMove
    {
        const double Speed = 30;
        const double HitDistance = 1.0;
        public event EventHandler<OnHitEventArgs>? OnHit;
        readonly IPlayerParamData? _playerParamData;
        readonly Func<AbstractEnemyMono?> _getNearestEnemyMono;
        Vector3 Direction { get; }
    
        readonly PlayerAttackEffectMono _playerAttackEffectMono;
        readonly PlayerAttackEffectViewMono? _playerAttackEffectViewMono;
        
        public  PlayerAttackLinear(
            PlayerAttackEffectMono playerAttackEffectMono
            , IPlayerParamData playerParamData
            , Func<AbstractEnemyMono?> getTargetEnemyMono
            , PlayerAttackEffectViewMono? playerAttackEffectViewMono
        )
        {
            _playerAttackEffectMono = playerAttackEffectMono;
            _playerParamData = playerParamData;
            _getNearestEnemyMono = getTargetEnemyMono;
            _playerAttackEffectViewMono = playerAttackEffectViewMono;
            var targetPosition = getTargetEnemyMono()?.transform.position ?? Vector3.zero;
            Direction = targetPosition != Vector3.zero ? (targetPosition - _playerAttackEffectMono.transform.position).normalized : Vector3.right;
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
        
            _playerAttackEffectMono.transform.position += Direction * (float)(Speed * Time.deltaTime);
            if (enemyMono != null)
            {
                if (enemyMono.transform.position.x - _playerAttackEffectMono.transform.position.x < HitDistance)
                {
                    var isTargetEnemy = PlayerAttackModule.GetTargetEnemyEnum(_playerParamData.PlayerEnum())
                        .Contains(enemyMono.EnemyEnum);
                    OnHit?.Invoke(this, new OnHitEventArgs(enemyMono, isTargetEnemy));
                    if (isTargetEnemy) UnityEngine.Object.Destroy(_playerAttackEffectMono.gameObject);
                    else Defenced(_playerAttackEffectViewMono);
                }
            }
            else
            {
                if (_playerAttackEffectMono.transform.position.x > 10) // todo: parameterからもらう
                    UnityEngine.Object.Destroy(_playerAttackEffectMono.gameObject);
            }
        }

        void Defenced(PlayerAttackEffectViewMono? playerAttackEffectViewMono)
        {
            _playerAttackEffectMono.transform.position -= new Vector3(1.2f, 0, 0); // 左にずらす
            if (playerAttackEffectViewMono != null)
                playerAttackEffectViewMono.HitNew(() => UnityEngine.Object.Destroy(_playerAttackEffectMono.gameObject));
            else UnityEngine.Object.Destroy(_playerAttackEffectMono.gameObject);
        }

 
    } 
}

