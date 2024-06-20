#nullable enable
using System;
using Daipan.Enemy.MonoScripts;
using Daipan.LevelDesign.Player.Scripts;
using Daipan.Player.Scripts;
using UnityEngine;

namespace Daipan.Player.MonoScripts
{
    public class PlayerAttackEffectMono : MonoBehaviour
    {
        [SerializeField] PlayerAttackEffectViewMono? viewMono;
        readonly double _speed = 10;
        public event EventHandler<OnHitEventArgs>? OnHit;

        EnemyMono? _targetEnemyMono;

        void Update()
        {
            var direction = Vector3.right;
            transform.position += direction * (float)(_speed * Time.deltaTime);
            if (_targetEnemyMono != null)
            {
                var enemyMono = _targetEnemyMono;
                if (enemyMono.transform.position.x - transform.position.x < float.Epsilon)
                {
                    OnHit?.Invoke(this, new OnHitEventArgs(enemyMono));
                    Destroy(gameObject);
                }
            }
            else
            {
                if (transform.position.x > 10) // todo: parameterからもらう
                    Destroy(gameObject);
            }
        }

        public void SetUp(PlayerParamData playerParamData, EnemyMono? targetEnemyMono)
        {
            Debug.Log($"PlayerAttackEffectMono data.Enum = {playerParamData.PlayerEnum()}");
            viewMono?.SetDomain(playerParamData);
            _targetEnemyMono = targetEnemyMono;
        }
    }

    public record OnHitEventArgs(EnemyMono? EnemyMono);
}