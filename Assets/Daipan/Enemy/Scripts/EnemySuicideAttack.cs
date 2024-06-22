#nullable enable
using Daipan.Battle.interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Player.MonoScripts;
using DG.Tweening;

namespace Daipan.Enemy.Scripts
{
    public class EnemySuicideAttack
    {
        readonly EnemyMono _enemyMono;
        readonly EnemyParamData _enemyParamData;
        public EnemySuicideAttack(
            EnemyMono enemyMono,
            EnemyParamData enemyParamData
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
                playerMono.SetHp(
                    new DamageArgs(_enemyParamData.GetAttackAmount(),
                        _enemyParamData.GetEnemyEnum())
                );

                // Optionally, you can destroy the enemy after the attack if required
                // Object.Destroy(_enemyMono.gameObject);
            });
        }
    }
}