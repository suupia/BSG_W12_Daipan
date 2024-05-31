#nullable enable
using System;
using Daipan.Battle.interfaces;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Enemy.MonoScripts
{

    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class EnemyMono : MonoBehaviour, IHpSetter
    {
        [SerializeField] HpGaugeMono hpGaugeMono = null!;
        EnemyAttack _enemyAttack = null!;
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
            _enemyAttack.AttackUpdate(_playerHolder.PlayerMono);

            // 攻撃範囲よりプレイヤーとの距離が大きいときだけ動く
            Debug.Log($"_enemyParamsConfig : {_enemyParamsConfig}");
            Debug.Log($"GetAttackParameter : { _enemyParamDataContainer.GetEnemyParamData(EnemyEnum).AttackRange()}"); 
            if (transform.position.x - _playerHolder.PlayerMono.transform.position.x >=
                _enemyParamDataContainer.GetEnemyParamData(EnemyEnum).AttackRange())
            {
                transform.position += Vector3.left * _enemyParamsConfig.GetSpeed(EnemyEnum) * Time.deltaTime;
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

        public void SetDomain(EnemyAttack enemyAttack)
        {
            _enemyAttack = enemyAttack;
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

        public void SetParameter(EnemyEnum enemyEnum)
        {
            EnemyEnum = enemyEnum;

            _enemyHp = new EnemyHp(_enemyParamDataContainer.GetEnemyParamData(EnemyEnum).GetCurrentHp(), this, _enemyCluster);

            //Sprite
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = _enemyParamsConfig.GetSprite(enemyEnum);
        }
    }

    public record DiedEventArgs(bool IsBoss, bool IsQuickDefeat, bool IsTrigger = false);
}
