#nullable enable
using Daipan.Battle.interfaces;
using Daipan.Enemy.MonoScripts;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public class EnemyHp : IEnemyHp
    {
        public int MaxHp { get; }
        public int CurrentHp { get; private set; }

        readonly EnemyMono _enemyMono;


        public EnemyHp(int maxHp, EnemyMono enemyMono)
        {
            MaxHp = maxHp;
            CurrentHp = MaxHp;

            _enemyMono = enemyMono;
        }

        public virtual void DecreaseHp(EnemyDamageArgs enemyDamageArgs)
        {
            CurrentHp -= enemyDamageArgs.DamageValue;
            Debug.Log($"Enemy CurrentHp : {CurrentHp}");
            if (CurrentHp <= 0) _enemyMono.Died(); 
        }
    }
}