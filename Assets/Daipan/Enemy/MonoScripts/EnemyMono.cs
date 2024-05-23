#nullable enable
using System;
using Daipan.Battle.interfaces;
using Daipan.Enemy.Interfaces;
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
        PlayerHolder _playerHolder = null!;
        EnemyParamsServer _enemyParamsServer = null!;
        public EnemyEnum _enemyEnum { get; private set; } = EnemyEnum.None;

         
        void Update()
        {
            _enemyAttack.AttackUpdate(_playerHolder.PlayerMono);

            transform.position += Vector3.left * _enemyParamsServer.GetSpeed(_enemyEnum) * Time.deltaTime;
            if (transform.position.x < _enemyParamsServer.GetDespawnedPosition().x) _enemyCluster.Remove(this, false); // Destroy when out of screen

            hpGaugeMono.SetRatio(CurrentHp / (float)_enemyParamsServer.GetHP(_enemyEnum));
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
            EnemyParamsServer enemyParamsServer
        )
        {
            _enemyCluster = enemyCluster;
            _playerHolder = playerHolder;
            _enemyParamsServer = enemyParamsServer;
        }

        public void SetDomain(EnemyAttack enemyAttack)
        {
            _enemyAttack = enemyAttack;
        }

        public void Died(bool isTriggerCallback)
        {
            if (isTriggerCallback)
            {
                var args = new DiedEventArgs(_enemyEnum.IsBoss);
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
            _enemyEnum = enemyEnum;
            _enemyAttack.enemyAttackParameter = _enemyParamsServer.GetAtatckParameter(enemyEnum);
            _enemyHp = new EnemyHp(_enemyParamsServer.GetHP(_enemyEnum), this, _enemyCluster);

            // Sprite
            //var spriteRenderer = GetComponent<SpriteRenderer>();
            //spriteRenderer.sprite = Parameter.sprite;

        }
    }

    public record DiedEventArgs(bool IsBoss, bool IsTrigger = false);
}