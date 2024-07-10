#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using DG.Tweening;
using R3;
using UnityEngine;
using VContainer;

namespace Daipan.Enemy.MonoScripts
{
    public sealed class EnemyMono : MonoBehaviour
    {
        public AbstractEnemyViewMono?  EnemyViewMono => enemyViewMono;
        [SerializeField] AbstractEnemyViewMono? enemyViewMono;
        EnemyCluster _enemyCluster = null!;
        EnemyAttackDecider _enemyAttackDecider = null!;
        EnemySuicideAttack _enemySuicideAttack = null!;
        EnemyDie _enemyDie = null!;
        IEnemySpawnPoint _enemySpawnPoint = null!;
        IEnemyParamContainer _enemyParamContainer = null!;
        PlayerHolder _playerHolder = null!;
        public EnemyEnum EnemyEnum { get; private set; } = EnemyEnum.None;
        public bool IsReachedPlayer { get; private set; }

        Hp _hp = null!;
        public Hp Hp => _hp; 

        void Update()
        {
            _playerHolder.PlayerMono.Hp = _enemyAttackDecider.AttackUpdate(this,_enemyParamContainer.GetEnemyParamData(EnemyEnum), _playerHolder.PlayerMono, enemyViewMono);

            // 攻撃範囲よりプレイヤーとの距離が大きいときだけ動く
            if (transform.position.x - _playerHolder.PlayerMono.transform.position.x >=
                _enemyParamContainer.GetEnemyParamData(EnemyEnum).GetAttackRange())
            {
                var moveSpeed = (float)_enemyParamContainer.GetEnemyParamData(EnemyEnum).GetMoveSpeedPerSec();
                transform.position += Time.deltaTime * moveSpeed * Vector3.left;
                IsReachedPlayer = false;
            }
            else
            {
                IsReachedPlayer = true;
            }

            if (transform.position.x < _enemySpawnPoint.GetEnemyDespawnedPoint().x)
               Die(this,isDaipaned:false);

            enemyViewMono?.SetHpGauge(_hp.Value, _enemyParamContainer.GetEnemyParamData(EnemyEnum).GetCurrentHp());
            
        }

        [Inject]
        public void Initialize(
            PlayerHolder playerHolder,
            IEnemySpawnPoint enemySpawnPointData,
            IEnemyParamContainer enemyParamContainer
        )
        {
            _playerHolder = playerHolder;
            _enemySpawnPoint = enemySpawnPointData;
            _enemyParamContainer = enemyParamContainer;
        }

        public void SetDomain(
            EnemyEnum enemyEnum
            ,EnemyCluster enemyCluster
            ,EnemyAttackDecider enemyAttackDecider
            ,EnemySuicideAttack enemySuicideAttack
            ,EnemyDie enemyDie
        )
        {
            EnemyEnum = enemyEnum;
            _enemyCluster = enemyCluster;
            _enemyAttackDecider = enemyAttackDecider;
            _enemySuicideAttack = enemySuicideAttack;
            _enemyDie = enemyDie;
            enemyViewMono?.SetDomain(_enemyParamContainer.GetEnemyViewParamData(EnemyEnum));
            _hp = new Hp(_enemyParamContainer.GetEnemyParamData(EnemyEnum).GetMaxHp());


            Observable
                .EveryValueChanged(_hp, x => x.Value)
                .Subscribe(_ =>
                {
                    if(_hp.Value <= 0) Die(this,isDaipaned:false);
                }).AddTo(this);
        }

        public void SuicideAttack(PlayerMono playerMono)
        {
            _enemySuicideAttack.SuicideAttack(playerMono);
        }
        
        public event EventHandler<DiedEventArgs>? OnDied
        {
            add => _enemyDie.OnDied += value;
            remove => _enemyDie.OnDied -= value;
        }

        public void Die(EnemyMono thisEnemyMono, bool isDaipaned = false)
        {
            _enemyCluster.Remove(thisEnemyMono);
            _enemyDie.Died(enemyViewMono, isDaipaned);

        }
    }

    public record DiedEventArgs(EnemyEnum EnemyEnum, bool IsTrigger = false);
}