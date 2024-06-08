#nullable enable
using System;
using UnityEngine;

namespace Daipan.Player.MonoScripts
{
    public class PlayerAttackEffectMono : MonoBehaviour
    {
        public Func<Vector3>? TargetPosition;
        double _speed = 2;
            
        void Update()
        {
            if (TargetPosition == null)
            {
                Debug.LogWarning("TargetPosition is null");
                return;
            }
            
            var direction = (TargetPosition() - transform.position).normalized;
            transform.position += (Vector3)direction * (float)(_speed * Time.deltaTime);
        }
    }
}