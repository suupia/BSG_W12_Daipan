#nullable enable
using System;
using UnityEngine;

namespace Daipan.Player.MonoScripts
{
    public class PlayerAttackEffectMono : MonoBehaviour
    {
        public Func<Vector3?>? TargetPosition;
        PlayerColor _playerColor;
        double _speed = 4;
        Vector3 TargetPositionCached { get; set; }
            
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
                Destroy(gameObject);
            }
        }

        public void SetDomain(PlayerColor playerColor)
        {
           _playerColor = playerColor; 
        }
    }
}