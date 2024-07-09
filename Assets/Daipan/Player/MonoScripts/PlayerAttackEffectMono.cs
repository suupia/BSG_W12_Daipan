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

        public event EventHandler<OnHitEventArgs>? OnHit
        {
            add => _playerAttackEffectMoveExecutor.OnHit += value;
            remove => _playerAttackEffectMoveExecutor.OnHit -= value;
        }

        PlayerAttackEffectMoveExecutor _playerAttackEffectMoveExecutor = null!;

        void Update()
        {
            _playerAttackEffectMoveExecutor.Move();
        }

        public void SetUp(IPlayerParamData playerParamData, Func<EnemyMono?> getTargetEnemyMono)
        {
            Debug.Log($"PlayerAttackEffectMono data.Enum = {playerParamData.PlayerEnum()}");
            viewMono?.SetDomain(playerParamData);
            _playerAttackEffectMoveExecutor = new PlayerAttackEffectMoveExecutor(
                this
                , playerParamData
                , getTargetEnemyMono
            );
        }
    }

    public record OnHitEventArgs(EnemyMono? EnemyMono, bool IsTargetEnemy);
    

}