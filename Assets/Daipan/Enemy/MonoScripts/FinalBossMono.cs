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
using UnityEngine.Serialization;
using VContainer;

namespace Daipan.Enemy.MonoScripts
{
    public sealed class FinalBossMono : MonoBehaviour, IEnemyMono
    {
        public GameObject GameObject => gameObject;
        public Transform Transform => transform;
        public AbstractFinalBossViewMono? FinalBossViewMono => finalBossViewMono;
        [SerializeField] AbstractFinalBossViewMono? finalBossViewMono;
        EnemyCluster _enemyCluster = null!;
        FinalBossActionDecider _finalBossActionDecider = null!;
        EnemyMove _enemyMove=null!;
        FinalBossDie _enemyDie = null!;
        IEnemySpawnPoint _enemySpawnPoint = null!;
        IFinalBossParamData _finalBossParamData = null!;
        IFinalBossViewParamData _finalBossViewParamData = null!;
        IEnemyOnAttacked _enemyOnAttacked = null!;
        PlayerHolder _playerHolder = null!;
        public EnemyEnum EnemyEnum { get;  set; } = EnemyEnum.None;
        public  bool IsReachedPlayer { get;  set; }
        Hp _hp = null!;

        public  Hp Hp
        {
            get => _hp;
             set
            {
                _hp = value;
                if (_hp.Value <= 0) Die();
            }
        }

        void Update()
        {
            if (Hp.Value != 0)
                IsReachedPlayer = _enemyMove.MoveUpdate(_playerHolder.PlayerMono.transform,
                    _finalBossParamData, finalBossViewMono); 

            if (transform.position.x < _enemySpawnPoint.GetEnemyDespawnedPoint().x)
                Die();

            Debug.Log($"FinalBossMono Hp.Value: {Hp.Value}");
            finalBossViewMono?.SetHpGauge(Hp.Value, _finalBossParamData.GetMaxHp());
        }

        [Inject]
        public void Initialize(
            PlayerHolder playerHolder
            , IEnemySpawnPoint enemySpawnPointData
            , IFinalBossParamData finalBossParamData
            , IFinalBossViewParamData finalBossViewParamData
        )
        {
            _playerHolder = playerHolder;
            _enemySpawnPoint = enemySpawnPointData;
            _finalBossParamData = finalBossParamData;
            _finalBossViewParamData = finalBossViewParamData;
        }

        public void SetDomain(
            EnemyEnum enemyEnum
            , EnemyCluster enemyCluster
            , FinalBossActionDecider finalBossActionDecider
            , FinalBossDie enemyDie
            , IEnemyOnAttacked enemyOnAttacked
        )
        {
            EnemyEnum = enemyEnum;
            _enemyCluster = enemyCluster;
            _finalBossActionDecider = finalBossActionDecider;
            _finalBossActionDecider.SetDomain(this, finalBossViewMono, _finalBossParamData, _playerHolder.PlayerMono);
            _enemyMove = new EnemyMove(transform);
            _enemyDie = enemyDie;
            _enemyOnAttacked = enemyOnAttacked;
            finalBossViewMono?.SetDomain(_finalBossViewParamData);
            Hp = new Hp(_finalBossParamData.GetMaxHp());
        }

        public event EventHandler<DiedEventArgs>? OnDiedEvent
        {
            add => _enemyDie.OnDied += value;
            remove => _enemyDie.OnDied -= value;
        }

        public event EventHandler<IPlayerParamData>? OnAttackedEvent;

        public  void OnAttacked(IPlayerParamData playerParamData)
        {
            Hp = _enemyOnAttacked.OnAttacked(Hp, playerParamData);
            OnAttackedEvent?.Invoke(this, playerParamData);
        }

        public  void OnDaipaned()
        {
            var daipanHitDamage =
                _finalBossParamData.GetDaipanHitDamagePercent() * 0.01 * _finalBossParamData.GetMaxHp();
            Hp = new Hp(Hp.Value - daipanHitDamage );
            transform.position += (float)_finalBossParamData.GetKnockBackDistance() * Vector3.right;
            finalBossViewMono?.DaipanHit();
        }

        void Die(bool isDaipaned = false)
        {
            _enemyCluster.Remove(this);
            _finalBossActionDecider.Dispose();
            
            _enemyDie.Died(finalBossViewMono, isDaipaned);
        }

        public  void Highlight(bool isHighlighted)
        {
            finalBossViewMono?.Highlight(isHighlighted);
        }
    }
}