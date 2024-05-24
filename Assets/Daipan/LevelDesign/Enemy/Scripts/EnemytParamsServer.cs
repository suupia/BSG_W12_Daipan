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
        readonly EnemyParamsManager _enemyParamsManager;
        readonly EnemyPosition _enemyPosition;

        [Inject]
        EnemyParamsServer (EnemyParamsManager enemyParamsManager, EnemyPosition enemyPosition)
        {
            _enemyParamsManager = enemyParamsManager;
            _enemyPosition = enemyPosition;
            
            CheckIsValid(_enemyParamsManager);
        }
        
        void CheckIsValid(EnemyParamsManager parameters)
        {
            foreach (var enemyLifeParam in parameters.enemyLifeParams)
            {
                var enemyParam = enemyLifeParam.enemyParams;
                if (enemyParam.attackAmount <= 0)
                {
                    Debug.LogWarning($"{enemyParam.GetEnemyEnum}の攻撃力が0以下です。");
                }
                if (enemyParam.attackRange <= 0)
                {
                    Debug.LogWarning($"{enemyParam.GetEnemyEnum}の攻撃範囲が0以下です。");
                }
            }
        }


        #region Params

        public float GetSpeed(EnemyEnum enemyEnum)
        {
            return GetEnemyParams(enemyEnum).moveSpeed_ups;
        }

        public EnemyAttackParameter GetAttackParameter(EnemyEnum enemyEnum)
        {
            var enemy = GetEnemyParams(enemyEnum);
            return new EnemyAttackParameter(
                enemy.attackAmount,
                enemy.attackDelaySec,
                enemy.attackRange
            );
        }

        public int GetHp(EnemyEnum enemyEnum)
        {
            return GetEnemyParams(enemyEnum).HPAmount; 
        }

        public Sprite GetSprite(EnemyEnum enemyEnum)
        {
            return GetEnemyParams(enemyEnum).sprite;
        }


        public void AddCurrentKillAmount()
        {
            _enemyParamsManager.currentKillAmount++;
        }
        /// <summary>
        /// 現在の状態に応じて生成する敵を決定
        /// </summary>
        /// <returns></returns>
        public EnemyEnum DecideRandomEnemyType()
        {
            // ボス発生条件を満たしていればBOSSを生成
            if (_enemyParamsManager.currentKillAmount >= _enemyParamsManager.spawnBossAmount)
            {
                _enemyParamsManager.currentKillAmount = 0;
                return EnemyEnum.Boss;
            }

            // 通常敵のType決め
            List<float> ratio = new();

            foreach (var enemyLife in _enemyParamsManager.enemyLifeParams)
            {
                ratio.Add(enemyLife.spawnRatio);
            }

            return _enemyParamsManager.enemyLifeParams[Randoms.RandomByRatio(ratio)].enemyParams.GetEnemyEnum;
        }

        EnemyParams GetEnemyParams(EnemyEnum enemyEnum)
        {
            return _enemyParamsManager.enemyLifeParams.First(c => c.enemyParams.GetEnemyEnum == enemyEnum).enemyParams;
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
        public int AttackAmount { get; private set; }
        public float AttackDelaySec { get; private set; }

        public float AttackRange{ get; private set; }

        public EnemyAttackParameter(int attackAmount, float attackDelaySec, float attackRange)
        {
            this.AttackAmount = attackAmount;
            this.AttackDelaySec = attackDelaySec;
            this.AttackRange = attackRange;
        }
    }
}