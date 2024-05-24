#nullable enable
using Daipan.Comment.Scripts;
using Daipan.LevelDesign.Comment.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Scripts;
using Daipan.Utility.Scripts;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;


namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyParamsServer
    {
        readonly EnemyManagerParams _enemyManagerParams = null;
        readonly EnemyPosition _enemyPosition = null;

        [Inject]
        EnemyParamsServer (EnemyManagerParams enemyManagerParams, EnemyPosition enemyPosition)
        {
            _enemyManagerParams = enemyManagerParams;
            _enemyPosition = enemyPosition;
        }


        #region Params

        public float GetSpeed(EnemyEnum enemyEnum)
        {
            return GetEnemyParams(enemyEnum).moveSpeed_ups;
        }

        public EnemyAttackParameter GetAtatckParameter(EnemyEnum enemyEnum)
        {
            var enemy = GetEnemyParams(enemyEnum);
            return new EnemyAttackParameter(
                enemy.attackAmount,
                enemy.attackDelaySec,
                enemy.attackRange
            );
        }

        public int GetHP(EnemyEnum enemyEnum)
        {
            return GetEnemyParams(enemyEnum).HPAmount; 
        }

        public Sprite GetSprite(EnemyEnum enemyEnum)
        {
            return GetEnemyParams(enemyEnum).sprite;
        }


        public EnemyEnum DecideRandomEnemyType()
        {
            // ボス発生条件を満たしていればBOSSを生成
            if (_enemyManagerParams.currentKillAmount >= _enemyManagerParams.spawnBossAmount)
            {
                _enemyManagerParams.currentKillAmount = 0;
                return EnemyEnum.Boss;
            }

            // 通常敵のType決め
            List<float> ratio = new();

            foreach (var enemyLife in _enemyManagerParams.enemyLifeParams)
            {
                ratio.Add(enemyLife.spawnRatio);
            }

            return _enemyManagerParams.enemyLifeParams[Randoms.RandomByRatio(ratio)].enemyParams.GetEnemyEnum;
        }

        EnemyParams GetEnemyParams(EnemyEnum enemyEnum)
        {
            return _enemyManagerParams.enemyLifeParams.First(c => c.enemyParams.GetEnemyEnum == enemyEnum).enemyParams;
        }

        #endregion

        #region Position

        public Vector3 GetSpawnedPositionRandom()
        {
            List<Vector3> position = new();
            List<float> ratio = new();

            foreach (var point in _enemyPosition.enemySpawnedPoints)
            {
                position.Add(point.transform.position);
                ratio.Add(point.ratio);
            }

            return position[Randoms.RandomByRatio(ratio)];
        }
        public Vector3 GetDespawnedPosition()
        {
            return _enemyPosition.enemyDespawnedPoint.position;
        }

        #endregion
        
    }

    public class EnemyAttackParameter
    {
        public int attackAmount { get; private set; }
        public float attackDelaySec { get; private set; }

        public float attackRange{ get; private set; }

        public EnemyAttackParameter(int attackAmount, float attackDelaySec, float attackRange)
        {
            this.attackAmount = attackAmount;
            this.attackDelaySec = attackDelaySec;
            this.attackRange = attackRange;
        }
    }
}