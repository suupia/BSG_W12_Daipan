#nullable enable
using System;
using Daipan.Battle.interfaces;
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
        // todo: SerializeFieldがあるのは嫌なので、EnmeyViewMonoを作成して、Viewに依存せずに処理を行えるようにする。
        [SerializeField] HpGaugeMono hpGaugeMono = null!;
        [SerializeField] SpriteRenderer spriteRenderer = null!;  // todo:すべてanimatorに置き換える。
        [SerializeField] Animator animator = null!;
        
        EnemyAttackDecider _enemyAttackDecider = null!;
        EnemyCluster _enemyCluster = null!;
        EnemyHp _enemyHp = null!;
        EnemyParamsConfig _enemyParamsConfig = null!;
        EnemyParamDataContainer _enemyParamDataContainer = null!;
        bool _isSlowDefeat;
        PlayerHolder _playerHolder = null!;
        EnemyQuickDefeatChecker _quickDefeatChecker = null!;
        EnemySlowDefeatChecker _slowDefeatChecker = null!;
        public EnemyEnum EnemyEnum { get; private set; } = EnemyEnum.None;

        void Update()
        {
            _enemyAttackDecider.AttackUpdate(_playerHolder.PlayerMono);

            // 攻撃範囲よりプレイヤーとの距離が大きいときだけ動く
            if (transform.position.x - _playerHolder.PlayerMono.transform.position.x >=
                _enemyParamDataContainer.GetEnemyParamData(EnemyEnum).GetAttackRange())
            {
                transform.position += Vector3.left * (float)_enemyParamsConfig.GetSpeedRate(EnemyEnum) * Time.deltaTime;
            }

            if (transform.position.x < _enemyParamsConfig.GetDespawnedPosition().x)
                _enemyCluster.Remove(this, false); // Destroy when out of screen

            hpGaugeMono.SetRatio(CurrentHp / (float)_enemyParamDataContainer.GetEnemyParamData(EnemyEnum).GetCurrentHp());

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
            EnemyParamsConfig enemyParamsConfig,
            EnemyParamDataContainer enemyParamDataContainer,
            EnemyQuickDefeatChecker quickDefeatChecker,
            EnemySlowDefeatChecker slowDefeatChecker
        )
        {
            _enemyCluster = enemyCluster;
            _playerHolder = playerHolder;
            _enemyParamsConfig = enemyParamsConfig;
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
            
            SetSprite(enemyEnum);
            SetAnimator(enemyEnum);
        }


        void SetSprite(EnemyEnum enemyEnum)
        {
            spriteRenderer.sprite = _enemyParamDataContainer.GetEnemyParamData(enemyEnum).GetSprite();
        }

        void SetAnimator(EnemyEnum enemyEnum)
        {
            // まだ何もしない
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
