#nullable enable
using System;
using Daipan.Battle.interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Enemy;
using UnityEngine;
using VContainer;

namespace Daipan.Enemy.MonoScripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class EnemyMono : MonoBehaviour, IHpSetter
    {
        [SerializeField] HpGaugeMono hpGaugeMono = null!;
        EnemyAttack _enemyAttack = null!;
        EnemyCluster _enemyCluster = null!;

        EnemyHp _enemyHp = null!;
        public EnemyParameter EnemyParameter { get; private set; } = null!;

        public IEnemyOnHit EnemyOnHit { get; private set; } = null!;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) _enemyAttack.Attack();
            if (Input.GetKeyDown(KeyCode.S)) EnemyOnHit.OnHit();

            transform.position += Vector3.left * EnemyParameter.movement.speed * Time.deltaTime;
            if (transform.position.x < -10) _enemyCluster.Remove(this); // Destroy when out of screen

            hpGaugeMono.SetRatio(CurrentHp / (float)EnemyParameter.hp.maxHp);
        }

        public int CurrentHp
        {
            set => _enemyHp.CurrentHp = value;
            get => _enemyHp.CurrentHp;
        }

        public event EventHandler<DiedEventArgs>? OnDied;

        public void BlownAway()
        {
            Debug.Log("Blown away");
            _enemyCluster.Remove(this);
        }

        public void Died()
        {
            var args = new DiedEventArgs(EnemyParameter.GetEnemyEnum.IsBoss);
            OnDied?.Invoke(this, args);
            Destroy(gameObject);
        }


        [Inject]
        public void Initialize(
            EnemyAttack enemyAttack,
            IEnemyOnHit enemyOnHit,
            EnemyCluster enemyCluster
        )
        {
            _enemyAttack = enemyAttack;
            EnemyOnHit = enemyOnHit;
            _enemyCluster = enemyCluster;
        }

        public void SetParameter(EnemyParameter enemyParameter)
        {
            EnemyParameter = enemyParameter;
            _enemyAttack.enemyAttackParameter = EnemyParameter.attack;
            _enemyHp = new EnemyHp(enemyParameter.hp.maxHp, this, _enemyCluster);

            // Sprite
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = EnemyParameter.sprite;

            var enemyOnHit = EnemyOnHit as EnemyOnHit;
            // enemyOnHit.ownEnemyType = _enemyParameter.enemyType;
        }
    }

    public record DiedEventArgs(bool IsBoss);
}