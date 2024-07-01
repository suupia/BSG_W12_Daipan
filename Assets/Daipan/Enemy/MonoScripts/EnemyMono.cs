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
    public sealed class EnemyMono : MonoBehaviour
    {
        public AbstractEnemyViewMono?  EnemyViewMono => enemyViewMono;
        [SerializeField] AbstractEnemyViewMono? enemyViewMono;
        EnemyAttackDecider _enemyAttackDecider = null!;
        EnemySuicideAttack _enemySuicideAttack = null!;
        EnemyDied _enemyDied = null!;
        IEnemyHp _enemyHp = null!;
        IEnemySpawnPoint _enemySpawnPoint = null!;
        IEnemyParamContainer _enemyParamContainer = null!;
        PlayerHolder _playerHolder = null!;
        public EnemyEnum EnemyEnum { get; private set; } = EnemyEnum.None;

        void Update()
        {
            var newPlayerHp = _enemyAttackDecider.AttackUpdate(this,_enemyParamContainer.GetEnemyParamData(EnemyEnum), _playerHolder.PlayerMono, enemyViewMono);
            _playerHolder.PlayerMono.PlayerHpNew = newPlayerHp;

            // 攻撃範囲よりプレイヤーとの距離が大きいときだけ動く
            if (transform.position.x - _playerHolder.PlayerMono.transform.position.x >=
                _enemyParamContainer.GetEnemyParamData(EnemyEnum).GetAttackRange())
            {
                var moveSpeed = (float)_enemyParamContainer.GetEnemyParamData(EnemyEnum).GetMoveSpeedPerSec();
                transform.position += Time.deltaTime * moveSpeed * Vector3.left;
            }

            if (transform.position.x < _enemySpawnPoint.GetEnemyDespawnedPoint().x)
               Died(isDaipaned:false); // Destroy when out of screen

            enemyViewMono?.SetHpGauge(CurrentHp, _enemyParamContainer.GetEnemyParamData(EnemyEnum).GetCurrentHp());
        }

        public int CurrentHp
        {
            get => _enemyHp.CurrentHp;
        }

        public void GetDamage(EnemyDamageArgs enemyDamageArgs)
        {
            _enemyHp.DecreaseHp(enemyDamageArgs);
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
            EnemyEnum enemyEnum,
            IEnemyHp enemyHp,
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
            enemyViewMono?.SetDomain(_enemyParamContainer.GetEnemyViewParamData(EnemyEnum));
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

    public record DiedEventArgs(EnemyEnum EnemyEnum, bool IsTrigger = false);
}