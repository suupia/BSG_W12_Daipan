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
        IEnemyMono _finalBossMono = null!;
        AbstractFinalBossViewMono? _finalBossViewMono;
        IFinalBossParamData _finalBossParamData = null!;
        PlayerMono _playerMono = null!;
        IDisposable? _summonEnemyDisposable;

        public FinalBossActionDecider(EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
        }

        public void SetDomain(
            IEnemyMono finalBossMono
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
            , EnemySpawner enemySpawner)
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
            , PlayerMono playerMono
        )
        {
            if (!CanAttack(enemyMono, enemyParamData, playerMono)) return;
            if (enemyViewMono != null) enemyViewMono.Attack();
            EnemyAttackModule.Attack(playerMono, enemyParamData);
        }

        static bool CanAttack(IEnemyMono enemyMono, IEnemyParamData enemyParamData, PlayerMono playerMono)
        {
            if (playerMono.Hp.Value <= 0) return false;
            if (enemyMono.Transform.position.x - playerMono.transform.position.x >
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