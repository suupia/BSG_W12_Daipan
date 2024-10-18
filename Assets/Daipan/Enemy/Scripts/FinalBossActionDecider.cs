#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using R3;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public sealed class FinalBossActionDecider : IDisposable
    {
        readonly IEnemySpawner _enemySpawner;
        readonly CompositeDisposable _disposable = new();
        IEnemyMono _finalBossMono = null!;
        AbstractFinalBossViewMono? _finalBossViewMono;
        IFinalBossParamData _finalBossParamData = null!;
        IPlayerMono _playerMono = null!;
        IDisposable? _summonEnemyDisposable;

        public FinalBossActionDecider(IEnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
        }

        public void SetDomain(
            IEnemyMono finalBossMono
            , AbstractFinalBossViewMono? finalBossViewMono
            , IFinalBossParamData finalBossParamData
            , IPlayerMono playerMono
        )
        {
            _finalBossMono = finalBossMono;
            _finalBossViewMono = finalBossViewMono;
            _finalBossParamData = finalBossParamData;
            _playerMono = playerMono;

            _disposable.Add(
                Observable
                    .Interval(TimeSpan.FromSeconds(finalBossParamData.GetSummonActionIntervalSec()))
                    .Subscribe(_ =>
                        SummonEnemy(finalBossParamData, finalBossViewMono, _enemySpawner))
            );

            _disposable.Add(
                Observable
                    .Interval(TimeSpan.FromSeconds(finalBossParamData.GetAttackIntervalSec()))
                    .Subscribe(_ => Attack(_finalBossMono, _finalBossViewMono, _finalBossParamData, _playerMono))
            );
        }

        void SummonEnemy(
            IFinalBossParamData finalBossParamData
            , AbstractFinalBossViewMono? finalBossViewMono
            , IEnemySpawner enemySpawner)
        {
            if (finalBossViewMono != null) finalBossViewMono.SummonEnemy();
            _disposable.Add(
                Observable
                    .Interval(TimeSpan.FromSeconds(finalBossParamData.GetSummonActionIntervalSec()))
                    .Subscribe(_ =>
                    {
                        var summonEnemyIntervalSec = finalBossParamData.GetSummonEnemyIntervalSec();
                        var summonEnemyCount = finalBossParamData.GetSummonEnemyCount();
                        _summonEnemyDisposable?.Dispose();
                        _summonEnemyDisposable =
                            Observable.Interval(TimeSpan.FromSeconds(summonEnemyIntervalSec))
                                .Take(summonEnemyCount)
                                .Subscribe(_ => { enemySpawner.SpawnEnemy(); });
                    })
            );
        }

        static void Attack(
            IEnemyMono enemyMono
            , AbstractFinalBossViewMono? enemyViewMono
            , IEnemyParamData enemyParamData
            , IPlayerMono playerMono
        )
        {
            if (!CanAttack(enemyMono, enemyParamData, playerMono)) return;
            if (enemyViewMono != null) enemyViewMono.Attack();
            EnemyAttackModule.Attack(playerMono, enemyParamData);
        }

        static bool CanAttack(IEnemyMono enemyMono, IEnemyParamData enemyParamData, IPlayerMono playerMono)
        {
            if (playerMono.Hp.Value <= 0) return false;
            if (enemyMono.Transform.position.x - playerMono.Transform.position.x >
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