#nullable enable
using System;
using Daipan.Battle.interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace Daipan.Enemy.MonoScripts
{
    public sealed class EnemyMono : MonoBehaviour, IHpSetter
    {
        [SerializeField] AbstractEnemyViewMono? enemyViewMono;
        EnemyAttackDecider _enemyAttackDecider = null!;
        EnemySuicideAttack _enemySuicideAttack = null!;
        EnemyDied _enemyDied = null!;
        EnemyCluster _enemyCluster = null!;
        EnemyHp _enemyHp = null!;
        IEnemySpawnPoint _enemySpawnPoint = null!;
        EnemyParamDataContainer _enemyParamDataContainer = null!;
        PlayerHolder _playerHolder = null!;
        public EnemyEnum EnemyEnum { get; private set; } = EnemyEnum.None;

        void Update()
        {
            _enemyAttackDecider.AttackUpdate(_playerHolder.PlayerMono, enemyViewMono);

            // 攻撃範囲よりプレイヤーとの距離が大きいときだけ動く
            if (transform.position.x - _playerHolder.PlayerMono.transform.position.x >=
                _enemyParamDataContainer.GetEnemyParamData(EnemyEnum).GetAttackRange())
            {
                var moveSpeed = (float)_enemyParamDataContainer.GetEnemyParamData(EnemyEnum).GetMoveSpeedPreSec();
                transform.position += Time.deltaTime * moveSpeed * Vector3.left;
            }

            if (transform.position.x < _enemySpawnPoint.GetEnemyDespawnedPoint().x)
               Died(isDaipaned:false); // Destroy when out of screen

            enemyViewMono?.SetHpGauge(CurrentHp, _enemyParamDataContainer.GetEnemyParamData(EnemyEnum).GetCurrentHp());
        }

        public int CurrentHp
        {
            set => _enemyHp.CurrentHp = value;
            get => _enemyHp.CurrentHp;
        }


        [Inject]
        public void Initialize(
            EnemyCluster enemyCluster,
            PlayerHolder playerHolder,
            IEnemySpawnPoint enemySpawnPointData,
            EnemyParamDataContainer enemyParamDataContainer
        )
        {
            _enemyCluster = enemyCluster;
            _playerHolder = playerHolder;
            _enemySpawnPoint = enemySpawnPointData;
            _enemyParamDataContainer = enemyParamDataContainer;
        }

        public void SetDomain(
            EnemyEnum enemyEnum,
            EnemyHp enemyHp,
            EnemyAttackDecider enemyAttackDecider,
            EnemySuicideAttack enemySuicideAttack,
            EnemyDied enemyDied
        )
        {
            EnemyEnum = enemyEnum;
            _enemyHp = enemyHp;
            _enemyAttackDecider = enemyAttackDecider;
            _enemySuicideAttack = enemySuicideAttack;
            _enemyDied = enemyDied;

            enemyViewMono?.SetDomain(_enemyParamDataContainer);
            enemyViewMono?.SetView(enemyEnum);
        }

        public void SuicideAttack(PlayerMono playerMono)
        {
            _enemySuicideAttack.SuicideAttack(playerMono);
        }
        
        public event EventHandler<DiedEventArgs>? OnDied
        {
            add => _enemyDied.OnDied += value;
            remove => _enemyDied.OnDied -= value;
        }
        
        public void Died(bool isDaipaned = false)
        {
            _enemyDied.Died(enemyViewMono, isDaipaned);
        }
    }

    public record DiedEventArgs(EnemyEnum enemyEnum, bool IsTrigger = false);
}