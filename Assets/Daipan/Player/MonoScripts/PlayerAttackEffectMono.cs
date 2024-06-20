#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Player.Scripts;
using Daipan.Player.Scripts;
using UnityEngine;

namespace Daipan.Player.MonoScripts
{
    public class PlayerAttackEffectMono : MonoBehaviour
    {
        [SerializeField] PlayerAttackEffectViewMono? viewMono;
        readonly double _speed = 10;
        readonly double _hitDistance = 0.1;
        public event EventHandler<OnHitEventArgs>? OnHit;
        PlayerParamData? _playerParamData;
        Func<EnemyMono?> _getNearestEnemyMono = () => null; 

        void Update()
        {
            // [Precondition]
            if (_playerParamData == null)
            {
                Debug.LogWarning("PlayerAttackEffectMono: PlayerParamData is null");
                return;
            }
            
            var direction = Vector3.right;
            transform.position += direction * (float)(_speed * Time.deltaTime);
            var enemyMono = _getNearestEnemyMono();
            if (enemyMono != null)
            {
                if (enemyMono.transform.position.x - transform.position.x < _hitDistance)
                {
                    var isTargetEnemy = PlayerAttackModule.GetTargetEnemyEnum(_playerParamData.PlayerEnum())
                        .Contains(enemyMono.EnemyEnum);
                    OnHit?.Invoke(this, new OnHitEventArgs(enemyMono, isTargetEnemy));
                    Destroy(gameObject);
 
                }
            }
            else
            {
                if (transform.position.x > 10) // todo: parameterからもらう
                    Destroy(gameObject);
            }
        }

        public void SetUp(PlayerParamData playerParamData, Func<EnemyMono?> getTargetEnemyMono)
        {
            Debug.Log($"PlayerAttackEffectMono data.Enum = {playerParamData.PlayerEnum()}");
            _playerParamData = playerParamData;
            viewMono?.SetDomain(playerParamData);
            _getNearestEnemyMono = getTargetEnemyMono;
        }
    }

    public record OnHitEventArgs(EnemyMono? EnemyMono, bool IsTargetEnemy);
    
    public static class PlayerAttackModule
    {
        public static IEnumerable<EnemyEnum> GetTargetEnemyEnum(PlayerColor playerColor)
        {
            return playerColor switch
            {
                PlayerColor.Red => new[] {EnemyEnum.Red,EnemyEnum.RedBoss,EnemyEnum.Special},
                PlayerColor.Blue => new[] {EnemyEnum.Blue,EnemyEnum.BlueBoss,EnemyEnum.Special},
                PlayerColor.Yellow => new[] {EnemyEnum.Yellow,EnemyEnum.YellowBoss,EnemyEnum.Special},
                _ => throw new ArgumentOutOfRangeException()
            };
        }

    }
}