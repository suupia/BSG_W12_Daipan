#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Player.Scripts;
using UnityEngine;
using R3;

namespace Daipan.Player.MonoScripts
{
    public sealed class PlayerAttackEffectMono : MonoBehaviour
    {
        [SerializeField] PlayerAttackEffectViewMono? viewMono;

        public event EventHandler<OnHitEventArgs>? OnHit
        {
            add => _playerAttackTracking.OnHit += value;
            remove => _playerAttackTracking.OnHit -= value;
        }

        IPlayerAttackMove _playerAttackTracking = null!;
        bool _isActive = true;

        void Update()
        {
            if(_isActive) _playerAttackTracking.Move();
            else
            {
                if ((viewMono?.IsFinishAnimation()) != false) Destroy(gameObject);
            }
        }

        public void SetUp(IPlayerParamData playerParamData, Func<AbstractEnemyMono?> getTargetEnemyMono)
        {
            Debug.Log($"PlayerAttackEffectMono data.Enum = {playerParamData.PlayerEnum()}");
            viewMono?.SetDomain(playerParamData);
            _playerAttackTracking = new PlayerAttackLinear(
                this
                , playerParamData
                , getTargetEnemyMono
            );
        }

        public void Defenced(Vector3 hitPosition)
        {
            transform.position = hitPosition - new Vector3(1f, 0, 0); ;
            _isActive = false;
            viewMono?.Hit();
        }
    }

}