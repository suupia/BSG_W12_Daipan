#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using R3;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public sealed class FinalBossActionDecider : IDisposable
    {
        readonly EnemySpawner _enemySpawner;
        readonly CompositeDisposable _disposable = new();
        FinalBossMono _finalBossMono = null!;
        AbstractFinalBossViewMono? _finalBossViewMono;
        IFinalBossParamData _finalBossParamData = null!;
        PlayerMono _playerMono = null!;

        public FinalBossActionDecider(EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
        }

        public void SetDomain(
            FinalBossMono finalBossMono
            , AbstractFinalBossViewMono? finalBossViewMono
            , IFinalBossParamData finalBossParamData
            , PlayerMono playerMono
        )
        {
            _finalBossMono = finalBossMono;
            _finalBossViewMono = finalBossViewMono;
            _finalBossParamData = finalBossParamData;
            _playerMono = playerMono;

            _disposable.Add(
                Observable
                    .Interval(TimeSpan.FromSeconds(finalBossParamData.GetSummonActionIntervalSec()))
                    .Subscribe(_ => SummonEnemy())
            );
            
            _disposable.Add(
                Observable
                    .Interval(TimeSpan.FromSeconds(finalBossParamData.GetAttackIntervalSec()))
                    .Subscribe(_ => Attack(_finalBossMono, _finalBossViewMono, _finalBossParamData, _playerMono))
            );
        }

        void SummonEnemy()
        {
            // todo: 敵を召喚する処理を書く
        }

        static void Attack(
            AbstractEnemyMono enemyMono
            , AbstractEnemyViewMono? enemyViewMono
            , IEnemyParamData enemyParamData
            , PlayerMono playerMono
        )
        {
            if (!CanAttack(enemyMono, enemyParamData, playerMono)) return;
            if (enemyViewMono != null) enemyViewMono.Attack();
            EnemyAttackModule.Attack(playerMono, enemyParamData);
        }

        static bool CanAttack(AbstractEnemyMono enemyMono, IEnemyParamData enemyParamData, PlayerMono playerMono)
        {
            if (playerMono.Hp.Value <= 0) return false;
            if (enemyMono.transform.position.x - playerMono.transform.position.x >
                enemyParamData.GetAttackRange()) return false;
            return true;
        }
        
        public void Dispose()
        {
            _disposable.Dispose();
        }
        ~FinalBossActionDecider()
        {
            Dispose();
        }
    }
}