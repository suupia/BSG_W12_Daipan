#nullable enable
using System;
using Daipan.Battle.interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace Daipan.Enemy.MonoScripts
{

    public sealed class EnemyMono : MonoBehaviour, IHpSetter
    {
        [SerializeField] AbstractEnemyViewMono? enemyViewMono;
        EnemyAttackDecider _enemyAttackDecider = null!;
        EnemyCluster _enemyCluster = null!;
        EnemyHp _enemyHp = null!;
        EnemyParamModifyWithTimer _enemyParamModifyWithTimer = null!;
        EnemySpawnPointData _enemySpawnPointData = null!;
        EnemyParamDataContainer _enemyParamDataContainer = null!;
        bool _isSlowDefeat;
        PlayerHolder _playerHolder = null!;
        EnemyQuickDefeatChecker _quickDefeatChecker = null!;
        EnemySlowDefeatChecker _slowDefeatChecker = null!;
        public EnemyEnum EnemyEnum { get; private set; } = EnemyEnum.None;

        void Update()
        {
            _enemyAttackDecider.AttackUpdate(_playerHolder.PlayerMono, enemyViewMono);

            // 攻撃範囲よりプレイヤーとの距離が大きいときだけ動く
            if (transform.position.x - _playerHolder.PlayerMono.transform.position.x >=
                _enemyParamDataContainer.GetEnemyParamData(EnemyEnum).GetAttackRange())
            {
                transform.position += Vector3.left * (float)_enemyParamModifyWithTimer.GetSpeedRate(EnemyEnum) * Time.deltaTime;
            }

            if (transform.position.x < _enemySpawnPointData.GetEnemyDespawnedPoint().x)
                _enemyCluster.Remove(this, false); // Destroy when out of screen

            enemyViewMono?.SetHpGauge(CurrentHp, _enemyParamDataContainer.GetEnemyParamData(EnemyEnum).GetCurrentHp());

            if (_isSlowDefeat == false && transform.position.x <= _slowDefeatChecker.SlowDefeatCoordinate)
            {
                _isSlowDefeat = true;
                _slowDefeatChecker.IncrementSlowDefeatCounter();
            }
        }

        public int CurrentHp
        {
            set => _enemyHp.CurrentHp = value;
            get => _enemyHp.CurrentHp;
        }

        public event EventHandler<DiedEventArgs>? OnDied;


        [Inject]
        public void Initialize(
            EnemyCluster enemyCluster,
            PlayerHolder playerHolder,
            EnemyParamModifyWithTimer enemyParamModifyWithTimer,
            EnemySpawnPointData enemySpawnPointData,
            EnemyParamDataContainer enemyParamDataContainer,
            EnemyQuickDefeatChecker quickDefeatChecker,
            EnemySlowDefeatChecker slowDefeatChecker
        )
        {
            _enemyCluster = enemyCluster;
            _playerHolder = playerHolder;
            _enemyParamModifyWithTimer = enemyParamModifyWithTimer;
            _enemySpawnPointData = enemySpawnPointData;
            _enemyParamDataContainer = enemyParamDataContainer;
            _quickDefeatChecker = quickDefeatChecker;
            _slowDefeatChecker = slowDefeatChecker;
        }

        public void SetDomain(
            EnemyEnum enemyEnum,
            EnemyHp enemyHp,
            EnemyAttackDecider enemyAttackDecider
            )
        {
            EnemyEnum = enemyEnum;
            _enemyHp = enemyHp;
            _enemyAttackDecider = enemyAttackDecider;
            
            enemyViewMono?.SetDomain(_enemyParamDataContainer);
            enemyViewMono?.SetView(enemyEnum);
        }

        public void Died(bool isTriggerCallback)
        {
            if (isTriggerCallback)
            {
                var isQuickDefeat = _quickDefeatChecker.IsQuickDefeat(transform.position);
                var args = new DiedEventArgs(EnemyEnum.IsBoss, isQuickDefeat);
                OnDied?.Invoke(this, args);
            }

            Destroy(gameObject);
        }


        public void BlownAway()
        {
            Debug.Log("Blown away");
            _enemyCluster.Remove(this);
        }


    }

    public record DiedEventArgs(bool IsBoss, bool IsQuickDefeat, bool IsTrigger = false);
}
