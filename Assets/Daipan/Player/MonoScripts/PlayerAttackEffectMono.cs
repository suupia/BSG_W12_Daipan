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
        public Func<Vector3?>? TargetPosition;
        PlayerParamData _playerParamData = null;
        EnemyMono _enemyMono;
        readonly double _speed = 10;
        Vector3 TargetPositionCached { get; set; }
        public event EventHandler<OnHitEventArgs>? OnHit;
            
        void Update()
        {
            if (TargetPosition == null)
            {
                Debug.LogWarning("TargetPosition is null");
                return;
            }
            if (TargetPosition() is {} targetPosition)
            {
                TargetPositionCached = targetPosition;
            }
            var direction = (TargetPositionCached - transform.position).normalized;
            transform.position += (Vector3)direction * (float)(_speed * Time.deltaTime);
            
            
            if (Vector3.Distance(transform.position, TargetPositionCached) < 0.1f) 
            {
                OnHit?.Invoke(this, new OnHitEventArgs(_enemyMono));
                Destroy(gameObject);
            }
        }

        public void SetDomain(PlayerParamData playerParamData)
        { 
            Debug.Log($"PlayerAttackEffectMono data.Enum = {playerParamData.PlayerEnum()}");
           _playerParamData = playerParamData; 
           viewMono?.SetDomain(playerParamData);
        }

        public void SetTarget(EnemyMono enemyMono)
        {
            _enemyMono = enemyMono;
        }
    }
    
    public record OnHitEventArgs(EnemyMono EnemyMono);
}