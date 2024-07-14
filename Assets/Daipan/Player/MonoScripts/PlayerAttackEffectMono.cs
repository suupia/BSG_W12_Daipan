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

        void Update()
        {
             _playerAttackTracking.Move();
        }

        public void SetUp(IPlayerParamData playerParamData, Func<AbstractEnemyMono?> getTargetEnemyMono)
        {
            Debug.Log($"PlayerAttackEffectMono data.Enum = {playerParamData.PlayerEnum()}");
            viewMono?.SetDomain(playerParamData);
            _playerAttackTracking = new PlayerAttackLinear(
                this
                , playerParamData
                , getTargetEnemyMono
                , viewMono
            );
        }

        public void Defenced()
        {
            _playerAttackTracking.Defenced(); 
        }
    }

}