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
        public Func<EnemyMono?> TargetEnemyMono = () => null;
        PlayerParamData _playerParamData = null;
        readonly double _speed = 10;
        Vector3 TargetPositionCached { get; set; }
        public event EventHandler<OnHitEventArgs>? OnHit;
            
        void Update()
        {
            if (TargetEnemyMono() == null)
            {
                Debug.LogWarning("TargetEnemyMono is null");
                return;
            }
            if (TargetEnemyMono() is {} enemyMono)
            {
                TargetPositionCached = enemyMono.transform.position;
            }
            // var direction = (TargetPositionCached - transform.position).normalized;
            var direction = Vector3.Project((TargetPositionCached - transform.position), Vector3.right).normalized;
            transform.position += (Vector3)direction * (float)(_speed * Time.deltaTime);
            
            
            if (Mathf.Abs(transform.position.x - TargetPositionCached.x) < 0.1f) 
            {
                OnHit?.Invoke(this, new OnHitEventArgs(TargetEnemyMono()));
                Destroy(gameObject);
            }
        }

        public void SetDomain(PlayerParamData playerParamData)
        { 
            Debug.Log($"PlayerAttackEffectMono data.Enum = {playerParamData.PlayerEnum()}");
           _playerParamData = playerParamData; 
           viewMono?.SetDomain(playerParamData);
        }
        
    }
    
    public record OnHitEventArgs(EnemyMono? EnemyMono);
}