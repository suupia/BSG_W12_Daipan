#nullable enable
using System;
using System.Linq;
using Daipan.Core.Interfaces;
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
        readonly Func<IEnemyMono?> _getNearestEnemyMono;
        readonly IMonoBehaviour _playerAttackEffectMono;
        readonly PlayerAttackEffectViewMono? _playerAttackEffectViewMono;
        Vector3 Direction { get; }
        bool IsHit { get; set; }

        public  PlayerAttackLinear(
            IMonoBehaviour playerAttackEffectMono
            , IPlayerParamData playerParamData
            , Func<IEnemyMono?> getTargetEnemyMono
            , PlayerAttackEffectViewMono? playerAttackEffectViewMono
        )
        {
            _playerAttackEffectMono = playerAttackEffectMono;
            _playerParamData = playerParamData;
            _getNearestEnemyMono = getTargetEnemyMono;
            _playerAttackEffectViewMono = playerAttackEffectViewMono;
            var targetPosition = getTargetEnemyMono()?.Transform.position ?? Vector3.zero;
            Direction = targetPosition != Vector3.zero ? (targetPosition - _playerAttackEffectMono.Transform.position).normalized : Vector3.right;
        }
        
        public void Move()
        {
            Debug.Log($"PlayerAttackLinear: Move");
            if (_playerAttackEffectMono == null) return;
            
            // [Precondition]
            if (_playerParamData == null)
            {
                Debug.LogWarning("PlayerAttackEffectMono: PlayerParamData is null");
                return;
            }
            
            if(IsHit) return;
        
            var enemyMono = _getNearestEnemyMono();
            if (enemyMono != null && !PlayerAttackModule.IsInStreamScreen(enemyMono.Transform.position))
                enemyMono = null; 
        
            _playerAttackEffectMono.Transform.position += Direction * (float)(Speed * Time.deltaTime);
            if (enemyMono != null)
            {
                if (enemyMono.Transform.position.x - _playerAttackEffectMono.Transform.position.x < HitDistance)
                {
                    var isTargetEnemy = PlayerAttackModule.GetTargetEnemyEnum(_playerParamData.PlayerEnum())
                        .Contains(enemyMono.EnemyEnum);
                    OnHit?.Invoke(this, new OnHitEventArgs(enemyMono, isTargetEnemy));
                    if (isTargetEnemy) UnityEngine.Object.Destroy(_playerAttackEffectMono.GameObject);
                    else Defenced();
                }
            }
            else
            {
                if (_playerAttackEffectMono.Transform.position.x > 10) // todo: parameterからもらう
                    UnityEngine.Object.Destroy(_playerAttackEffectMono.GameObject);
            }
        }

        public void Defenced()
        {
            IsHit = true;
            _playerAttackEffectMono.Transform.position -= new Vector3(0.2f, 0, 0); // すこし左にずらす
            if (_playerAttackEffectViewMono != null)
                _playerAttackEffectViewMono.Hit(() => UnityEngine.Object.Destroy(_playerAttackEffectMono.GameObject));
            else UnityEngine.Object.Destroy(_playerAttackEffectMono.GameObject);
        }

 
    } 
}

