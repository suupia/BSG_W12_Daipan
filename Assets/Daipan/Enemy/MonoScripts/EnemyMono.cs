#nullable enable
using System;
using Daipan.Battle.interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
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
        public EnemyParameter Parameter { get; private set; } = null!;

        public IEnemyOnHit EnemyOnHit { get; private set; } = null!;

        void Update()
        {
            _enemyAttack.Update(_playerHolder.PlayerMono);
            if (Input.GetKeyDown(KeyCode.S)) EnemyOnHit.OnHit();

            transform.position += Vector3.left * Parameter.movement.speed * Time.deltaTime;
            if (transform.position.x < -10) _enemyCluster.Remove(this); // Destroy when out of screen

            hpGaugeMono.SetRatio(CurrentHp / (float)Parameter.hp.maxHp);
        }

        public int CurrentHp
        {
            set => _enemyHp.CurrentHp = value;
            get => _enemyHp.CurrentHp;
        }

        public event EventHandler<DiedEventArgs>? OnDied;


        [Inject]
        public void Initialize(
            IEnemyOnHit enemyOnHit,
            EnemyCluster enemyCluster,
            PlayerHolder playerHolder
        )
        {
            EnemyOnHit = enemyOnHit;
            _enemyCluster = enemyCluster;
            _playerHolder = playerHolder;
        }

        public void SetDomain(EnemyAttack enemyAttack)
        {
            _enemyAttack = enemyAttack;
        }

        public void Died()
        {
            var args = new DiedEventArgs(Parameter.GetEnemyEnum.IsBoss);
            OnDied?.Invoke(this, args);
            Destroy(gameObject);
        }


        public void BlownAway()
        {
            Debug.Log("Blown away");
            _enemyCluster.Remove(this);
        }

        public void SetParameter(EnemyParameter enemyParameter)
        {
            Parameter = enemyParameter;
            _enemyAttack.enemyAttackParameter = Parameter.attack;
            _enemyHp = new EnemyHp(enemyParameter.hp.maxHp, this, _enemyCluster);

            // Sprite
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = Parameter.sprite;

            var enemyOnHit = EnemyOnHit as EnemyOnHit;
            // enemyOnHit.ownEnemyType = _enemyParameter.enemyType;
        }
    }

    public record DiedEventArgs(bool IsBoss);
}