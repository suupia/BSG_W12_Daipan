#nullable enable
using System;
using System.Linq;
using Daipan.Daipan;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using DG.Tweening;
using Fusion;
using R3;
using UnityEngine;
using VContainer;

namespace Daipan.Enemy.MonoScripts
{
    public sealed class EnemyNet : NetworkBehaviour, IEnemyMono, IEnemyInitializer, IEnemySetDomain
    {
        public GameObject GameObject => gameObject;
        public Transform Transform => transform;
        [SerializeField] EnemyViewNet? enemyViewMono;
        IEnemyCluster _enemyCluster = null!;
        EnemyMove _enemyMove = null!;
        EnemyAttackDecider _enemyAttackDecider = null!;
        EnemyDie _enemyDie = null!;
        IEnemyOnAttacked _enemyOnAttacked = null!;
        IEnemyOnDied _enemyOnDied = null!;
        PlayerHolder? _playerHolder;
        IEnemySpawnPoint? _enemySpawnPoint;
        IEnemyParamContainer? _enemyParamContainer;
        [Networked]
        [OnChangedRender(nameof(OnEnemyEnumChanged))]
        public EnemyEnum EnemyEnum { get; set; } = EnemyEnum.None;
        
        [Networked]
        [OnChangedRender(nameof(OnIsSpawnedByAntiChanged))]
        NetworkBool IsSpawnedByAnti { get; set; }

        public bool IsReachedPlayer { get; set; }
        [Networked] 
        [Capacity(4)]
        NetworkLinkedList<PlayerRef> IsDiedList => default;

        Hp _hp; 

        public Hp Hp
        {
            get => _hp;
            set
            {
                _hp = value;
                if (_hp.Value <= 0) Die();
            }
        }

        // EnemyViewの初期化のためのフラグ
        bool _isCallOnChangedEnemyEnumChanged;
        bool _isCallOnChangedIsSpawnedByAntiChanged;

        public event EventHandler<IPlayerParamData>? OnAttackedEvent;

        public override void Spawned()
        {
            base.Spawned();
            Debug.Log($"EnemyNet Spawned");

            if (!HasStateAuthority)
            {
                var daipanScopeNet = DaipanScopeNet.BuiltContainer;
                Initialize(
                    daipanScopeNet.Container.Resolve<PlayerHolder>()
                    , daipanScopeNet.Container.Resolve<IEnemySpawnPoint>()
                    , daipanScopeNet.Container.Resolve<IEnemyParamContainer>()
                );
            }

            OnEnemyEnumChanged();
            OnIsSpawnedByAntiChanged();
        }

        public override void FixedUpdateNetwork()
        {
            _enemyAttackDecider.AttackUpdate(this, enemyViewMono,
                _enemyParamContainer.GetEnemyParamData(EnemyEnum), _playerHolder.PlayerMono);

            IsReachedPlayer = _enemyMove.MoveUpdate(Runner.DeltaTime, _playerHolder.PlayerMono.Transform, _enemyParamContainer.GetEnemyParamData(EnemyEnum), enemyViewMono);
            if (EnemyEnum.IsSpecial() == true && IsReachedPlayer)
            {
                // Special Enemy Die
                Debug.Log("Special enemy die by ReachedPlayer");
                DieBySpecialBlack();
            }
            
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
            Debug.Log($"EnemyNet Initialize");
            _playerHolder = playerHolder;
            _enemySpawnPoint = enemySpawnPointData;
            _enemyParamContainer = enemyParamContainer;
        }

        public void SetDomain(
            EnemyEnum enemyEnum
            , IEnemyCluster enemyCluster
            , EnemyAttackDecider enemyAttackDecider
            , EnemyDie enemyDie
            , IEnemyOnAttacked enemyOnAttacked
            , IEnemyOnDied enemyOnDied
        )
        {
            Debug.Log($"EnemyNet SetDomain");
            EnemyEnum = enemyEnum;
            _enemyCluster = enemyCluster;
            _enemyMove = new EnemyMove(transform);
            _enemyAttackDecider = enemyAttackDecider;
            _enemyDie = enemyDie;
            _enemyOnAttacked = enemyOnAttacked;
            _enemyOnDied = enemyOnDied;
            Hp = new Hp(_enemyParamContainer.GetEnemyParamData(EnemyEnum).GetMaxHp());
        }
        
        public void SetIsSpawnedByAnti(bool isSpawnedByAnti)
        {
            IsSpawnedByAnti = isSpawnedByAnti;
        }

        public event EventHandler<DiedEventArgs>? OnDiedEvent
        {
            add => _enemyDie.OnDied += value;
            remove => _enemyDie.OnDied -= value;
        }

        public void Highlight(bool isHighlighted)
        {
            enemyViewMono?.Highlight(isHighlighted);
        }
        
        public void DeleteSelf()
        {
            RpcDeleteSelf();
        }
        
        [Rpc(RpcSources.All, RpcTargets.All)]
        void RpcDeleteSelf()
        {
            DeleteSelfBody();
        }
        void DeleteSelfBody()
        {
            Debug.Log("[EnemyNet] DeleteSelf. LocalPlayer : " + Runner.LocalPlayer);
            IsDiedList.Add(Runner.LocalPlayer);
            Debug.Log($"[EnemyNet] IsDiedList.Count : {IsDiedList.Count}, Runner.ActivePlayers.Count() : {Runner.ActivePlayers.Count()}");
            if (IsDiedList.Count == Runner.ActivePlayers.Count())
            {
                Debug.Log("[EnemyNet] All players died");
                RpcDespawn(); 
            } 
        }
        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        void RpcDespawn()
        {
            Runner.Despawn(Object);
        }

        public void OnAttacked(IPlayerParamData playerParamData)
        {
            // Hpの増減より先に判定する必要がある
            if (EnemyEnum.IsSpecial() == true && 
                !EnemySpecialOnAttacked.IsSameColor(EnemyEnum, playerParamData.PlayerEnum()))
            {
                // Die
                Debug.Log($"Special enemy die. enemyViewMono : {enemyViewMono}, EnemyEnum : {EnemyEnum}");
                DieBySpecialBlack();
            }

            Hp = _enemyOnAttacked.OnAttacked(Hp, playerParamData);
        }

        public void OnDaipaned()
        {
            Debug.Log("Enemy is dead");
            _enemyCluster.Remove(this);
            _enemyDie.DiedByDaipan(enemyViewMono);
        }

        void Die()
        {
            Debug.Log("Enemy is dead");
            _enemyOnDied.OnDied(); // Destroyする前の方がいいはず
            _enemyCluster.Remove(this);
            _enemyDie.Died(enemyViewMono);
        }
        void DieBySpecialBlack()
        {
            _enemyCluster.Remove(this);
            _enemyDie.DiedBySpecialBlack(enemyViewMono?.GetSelectedEnemyViewMono);
        }

        // OnChangeRender functions 
        void OnEnemyEnumChanged()
        {
            Debug.Log($"[EnemyNet] OnEnemyEnumChanged() EnemyEnum : {EnemyEnum}");
            _isCallOnChangedEnemyEnumChanged = true;
            TryEnemyViewMonoSetDomain();
        }

        void OnIsSpawnedByAntiChanged()
        {
            Debug.Log($"[EnemyNet] OnIsSpawnedByAntiChanged() IsSpawnedByAnti : {IsSpawnedByAnti}");
            _isCallOnChangedIsSpawnedByAntiChanged = true;
            TryEnemyViewMonoSetDomain();
        }
        
        void TryEnemyViewMonoSetDomain()
        {
            if (_isCallOnChangedEnemyEnumChanged && _isCallOnChangedIsSpawnedByAntiChanged)
            {
                EnemyViewMonoSetDomain();
            }
        }

        void EnemyViewMonoSetDomain()
        {
            Debug.Log($"[EnemyNet] EnemyViewMonoSetDomain() EnemyEnum : {EnemyEnum}, IsSpawnedByAnti : {IsSpawnedByAnti}");
            if (_enemyParamContainer == null)
            {
                Debug.LogWarning($"_enemyParamContainer is null");
                return;
            }

            if (EnemyEnum == EnemyEnum.None) return;
            var enemyViewParamData = _enemyParamContainer.GetEnemyViewParamData(EnemyEnum);
            enemyViewParamData.IsSpawnedByAnti = IsSpawnedByAnti;
            enemyViewMono?.SetOnDiedCallback(DeleteSelf);
            enemyViewMono?.SetOnDaipanedCallback(OnDaipaned);
            enemyViewMono?.SetSpecialBlackCallback(DeleteSelf); 
        }
    }
}