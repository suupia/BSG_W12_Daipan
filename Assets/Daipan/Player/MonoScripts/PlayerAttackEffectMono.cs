#nullable enable
using System;
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
        double _speed = 10;
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

        public void SetDomain(PlayerParamData playerPramaData)
        {
           _playerParamData = playerPramaData; 
           viewMono?.SetDomain(playerPramaData);
        }
    }
}