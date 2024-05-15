#nullable enable
using Daipan.Battle.interfaces;
using Daipan.Enemy.MonoScripts;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public class EnemyHp : IHpSetter
    {
        int _currentHp;
        readonly EnemyMono _enemyMono;
        readonly EnemyCluster _enemyCluster;

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
                if (_currentHp <= 0)
                {
                    _enemyCluster.RemoveEnemy(_enemyMono);
                }
            }
        }
    }
}