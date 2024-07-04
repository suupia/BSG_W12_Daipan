#nullable enable
using System.Net.NetworkInformation;
using UnityEngine;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using DG.Tweening;

namespace Daipan.Enemy.Scripts
{
    public class EnemySuicideAttack
    {
        readonly EnemyMono _enemyMono;
        readonly IEnemyParamData _enemyParamData;

        public EnemySuicideAttack(
            EnemyMono enemyMono,
            IEnemyParamData enemyParamData
        )
        {
            _enemyMono = enemyMono;
            _enemyParamData = enemyParamData;
        }

        public void SuicideAttack(PlayerMono playerMono)
        {
            // Move towards playerMono's position using DoTween
            _enemyMono.transform.DOMove(playerMono.transform.position, 1f).OnComplete(() =>
            {
                // After reaching the player, apply damage
                playerMono.Hp = new Hp(playerMono.Hp.Value - _enemyParamData.GetAttackAmount());

                // Optionally, you can destroy the enemy after the attack if required
                _enemyMono.Remove(_enemyMono);
            });
        }
    }
}