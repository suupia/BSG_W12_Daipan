#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using DG.Tweening;
using R3;
using UnityEngine;
using VContainer;

namespace Daipan.Enemy.MonoScripts
{
    public sealed class EnemyMono : AbstractEnemyMono
    {
        public EnemyViewMono? EnemyViewMono => enemyViewMono;
        [SerializeField] EnemyViewMono? enemyViewMono;
        EnemyCluster _enemyCluster = null!;
        EnemyMove _enemyMove = null!;
        EnemyAttackDecider _enemyAttackDecider = null!;
        EnemyDie _enemyDie = null!;
        IEnemySpawnPoint _enemySpawnPoint = null!;
        IEnemyParamContainer _enemyParamContainer = null!;
        IEnemyOnAttacked _enemyOnAttacked = null!;
        PlayerHolder _playerHolder = null!;
        public override EnemyEnum EnemyEnum { get; protected set; } = EnemyEnum.None;
        public override bool IsReachedPlayer { get; protected set; }
        Hp _hp = null!;

        public override Hp Hp
        {
            get => _hp;
            protected set
            {
                _hp = value;
                if (_hp.Value <= 0) Die();
            }
        }


        void Update()
        {
            _enemyAttackDecider.AttackUpdate(this, enemyViewMono,
                _enemyParamContainer.GetEnemyParamData(EnemyEnum), _playerHolder.PlayerMono);

            IsReachedPlayer = _enemyMove.MoveUpdate(_playerHolder.PlayerMono.transform,
                _enemyParamContainer.GetEnemyParamData(EnemyEnum), enemyViewMono);

            if (transform.position.x < _enemySpawnPoint.GetEnemyDespawnedPoint().x) Die();

            enemyViewMono?.SetHpGauge(Hp.Value, _enemyParamContainer.GetEnemyParamData(EnemyEnum).GetMaxHp());
        }

        [Inject]
        public void Initialize(
            PlayerHolder playerHolder
            , IEnemySpawnPoint enemySpawnPointData
            , IEnemyParamContainer enemyParamContainer
        )
        {
            _playerHolder = playerHolder;
            _enemySpawnPoint = enemySpawnPointData;
            _enemyParamContainer = enemyParamContainer;
        }

        public void SetDomain(
            EnemyEnum enemyEnum
            , EnemyCluster enemyCluster
            , EnemyAttackDecider enemyAttackDecider
            , EnemyDie enemyDie
            , IEnemyOnAttacked enemyOnAttacked
        )
        {
            EnemyEnum = enemyEnum;
            _enemyCluster = enemyCluster;
            _enemyMove = new EnemyMove(transform);
            _enemyAttackDecider = enemyAttackDecider;
            _enemyDie = enemyDie;
            _enemyOnAttacked = enemyOnAttacked;
            enemyViewMono?.SetDomain(_enemyParamContainer.GetEnemyViewParamData(EnemyEnum));
            Hp = new Hp(_enemyParamContainer.GetEnemyParamData(EnemyEnum).GetMaxHp());

        }

        public event EventHandler<DiedEventArgs>? OnDied
        {
            add => _enemyDie.OnDied += value;
            remove => _enemyDie.OnDied -= value;
        }

        public override void Highlight(bool isHighlighted)
        {
            EnemyViewMono?.Highlight(isHighlighted);
        }

        public override void OnAttacked(IPlayerParamData playerParamData)
        {
            // Hpの増減より先に判定する必要がある
            if (EnemyEnum.IsSpecial() == true &&
                !EnemySpecialOnAttacked.IsSameColor(EnemyEnum, playerParamData.PlayerEnum()))
            {
                // Die
                Debug.Log("Special enemy die");
                _enemyCluster.Remove(this);
                _enemyDie.DiedBySpecialBlack(enemyViewMono);
            }

            Hp = _enemyOnAttacked.OnAttacked(Hp, playerParamData);
        }

        public override void OnDaipaned()
        {
            _enemyCluster.Remove(this);
            _enemyDie.DiedByDaipan(enemyViewMono);
        }

        void Die()
        {
            _enemyCluster.Remove(this);
            _enemyDie.Died(enemyViewMono);
        }
    }
}