#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Player.Scripts;
using UnityEngine;

namespace Daipan.Player.MonoScripts
{
    public sealed class PlayerAttackEffectMono : MonoBehaviour
    {
        [SerializeField] PlayerAttackEffectViewMono? viewMono;
        readonly double _speed = 30;
        readonly double _hitDistance = 1.0;
        public event EventHandler<OnHitEventArgs>? OnHit;
        IPlayerParamData? _playerParamData;
        Func<EnemyMono?> _getNearestEnemyMono = () => null;

        Vector3 Direction { get; set; } = Vector3.right;

        void Update()
        {
            // [Precondition]
            if (_playerParamData == null)
            {
                Debug.LogWarning("PlayerAttackEffectMono: PlayerParamData is null");
                return;
            }

            var enemyMono = _getNearestEnemyMono();

            if(enemyMono != null)
            {
                if (!PlayerAttackModule.IsInScreenEnemy(enemyMono))
                {
                    enemyMono = null;
                }
                
            }

            Direction = enemyMono != null ? (enemyMono.transform.position - transform.position).normalized : Direction;
            transform.position += Direction * (float)(_speed * Time.deltaTime);
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

        public void SetUp(IPlayerParamData playerParamData, Func<EnemyMono?> getTargetEnemyMono)
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
                PlayerColor.Red => new[] {EnemyEnum.Red,EnemyEnum.RedBoss,EnemyEnum.Special,EnemyEnum.Totem},
                PlayerColor.Blue => new[] {EnemyEnum.Blue,EnemyEnum.BlueBoss,EnemyEnum.Special,EnemyEnum.Totem},
                PlayerColor.Yellow => new[] {EnemyEnum.Yellow,EnemyEnum.YellowBoss,EnemyEnum.Special,EnemyEnum.Totem},
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static bool IsInScreenEnemy(EnemyMono enemyMono)
        {
            var worldPos = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            return enemyMono.gameObject.transform.position.x < worldPos.x;
        }
    }

}