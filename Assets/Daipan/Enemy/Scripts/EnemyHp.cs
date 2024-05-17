#nullable enable
using Daipan.Battle.interfaces;
using Daipan.Enemy.MonoScripts;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyHp : IHpSetter
    {
        readonly EnemyCluster _enemyCluster;
        readonly EnemyMono _enemyMono;
        int _currentHp;

        public EnemyHp(int maxHp, EnemyMono enemyMono, EnemyCluster enemyCluster)
        {
            CurrentHp = maxHp;
            _enemyMono = enemyMono;
            _enemyCluster = enemyCluster;
        }

        public int CurrentHp
        {
            get => _currentHp;
            set
            {
                _currentHp = value;
                Debug.Log($"Enemy CurrentHp : {_currentHp}");
                if (_currentHp <= 0) _enemyCluster.Remove(_enemyMono);
            }
        }
    }
}