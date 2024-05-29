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
    public class EnemyParamsConfig
    {
        readonly Timer _timer;
        readonly EnemyParamsManager _enemyParamsManager;
        readonly EnemyPositionMono _enemyPositionMono;

        [Inject]
        EnemyParamsConfig(
            EnemyParamsManager enemyParamsManager,
            EnemyPositionMono enemyPositionMono,
            Timer timer)
        {
            _enemyParamsManager = enemyParamsManager;
            _enemyPositionMono = enemyPositionMono;
            _timer = timer;

            CheckIsValid(_enemyParamsManager);
        }

        void CheckIsValid(EnemyParamsManager parameters)
        {
            Debug.Log($"EnemeyCount : {parameters.enemyParams.Count}");
            foreach (var enemyParam in parameters.enemyParams)
            {
                if (enemyParam.enemyAttackParam.attackAmount <= 0)
                {
                    Debug.LogWarning($"{enemyParam.GetEnemyEnum}の攻撃力が0以下です。");
                }
                if (enemyParam.enemyAttackParam.attackRange <= 0)
                {
                    Debug.LogWarning($"{enemyParam.GetEnemyEnum}の攻撃範囲が0以下です。");
                }
            }
        }


        #region Params

        public float GetSpeed(EnemyEnum enemyEnum)
        {
            return GetEnemyParams(enemyEnum).enemyMoveParam.moveSpeedPerSec * GetEnemyTimeLineParam().moveSpeedRate;
        }

        public EnemyAttackParameter GetAttackParameter(EnemyEnum enemyEnum)
        {
            var enemy = GetEnemyParams(enemyEnum);
            return new EnemyAttackParameter(
                enemy.enemyAttackParam.attackAmount,
                enemy.enemyAttackParam.attackDelaySec,
                enemy.enemyAttackParam.attackRange
            );
        }

        public int GetHp(EnemyEnum enemyEnum)
        {
            return GetEnemyParams(enemyEnum).enemyHpParam.hpAmount;
        }

        public Sprite GetSprite(EnemyEnum enemyEnum)
        {
            return GetEnemyParams(enemyEnum).sprite;
        }

        public float GetSpawnDelaySec()
        {
            return GetEnemyTimeLineParam().spawnDelaySec;
        }

        public int GetSpawnBossAmount()
        {
            return GetEnemyTimeLineParam().spawnBossAmount;
        }
        public void AddCurrentKillAmount()
        {
            _enemyParamsManager.currentKillAmount++;
        }

        public void ResetCurrentKillAmount()
        {
            _enemyParamsManager.currentKillAmount = 0;
        }
        /// <summary>
        /// 現在の状態に応じて生成する敵を決定
        /// </summary>
        /// <returns></returns>
        public EnemyEnum DecideRandomEnemyType()
        {
            // ボス発生条件を満たしていればBOSSを生成
            if (_enemyParamsManager.currentKillAmount >= GetSpawnBossAmount())
            {
                ResetCurrentKillAmount();
                return EnemyEnum.Boss;
            }

            // 通常敵のType決め
            List<float> ratio = new();

            foreach (var enemyLife in _enemyParamsManager.enemyParams)
            {
                if(enemyLife.GetEnemyEnum == EnemyEnum.Boss) continue;
                ratio.Add(enemyLife.enemySpawnParam.spawnRatio);
            }
            // ここで100%に正規化
            

            Debug.Log($"enemyPrams.Length : {_enemyParamsManager.enemyParams.Count}");
            Debug.Log($"Randoms.RandomByRatio(ratio) : {Randoms.RandomByRatio(ratio)}");
            return _enemyParamsManager.enemyParams[Randoms.RandomByRatio(ratio)].GetEnemyEnum;
        }
        /// <summary>
        /// Bossを含めないスポーン確率の入ったリストを受け取り、Bossを含めて返す
        /// </summary>
        List<float> NormalizeEnemySpawnRatioWithBoss(List<float> ratio)
        {
            float beforeRatio = 0f;
            foreach (var f in ratio)
            {
                beforeRatio += f;
            }
            float afterRatio = (100f - GetEnemyTimeLineParam().spawnBossRatio) / beforeRatio;

            var ret = ratio.Select(x => x * afterRatio).ToList();
            ret.Add(GetEnemyTimeLineParam().spawnBossRatio);
            return ret;
        }

        EnemyParam GetEnemyParams(EnemyEnum enemyEnum)
        {
            return _enemyParamsManager.enemyParams.First(c => c.GetEnemyEnum == enemyEnum);
        }

        EnemyTimeLineParam GetEnemyTimeLineParam()
        {
            if (_enemyParamsManager.enemyTimeLines.Count == 0)
            {
                var etlp = new EnemyTimeLineParam();
                etlp.spawnDelaySec = _enemyParamsManager.spawnDelaySec;
                etlp.moveSpeedRate = 1f;
                etlp.spawnBossAmount = _enemyParamsManager.spawnBossAmount;
                return etlp;
            }
            else
            {
                var i = _enemyParamsManager.enemyTimeLines.Where(e => e.startTime <= _timer.GetCurrentTime())
                   .OrderByDescending(e => e.startTime).First();
                return i;
            }
        }

        #endregion

        #region Position

        public Vector3 GetSpawnedPositionRandom()
        {
            List<Vector3> position = new();
            List<float> ratio = new();

            foreach (var point in _enemyPositionMono.enemySpawnedPoints)
            {
                position.Add(point.transform.position);
                ratio.Add(point.ratio);
            }

            return position[Randoms.RandomByRatio(ratio)];
        }
        public Vector3 GetDespawnedPosition()
        {
            return _enemyPositionMono.enemyDespawnedPoint.position;
        }

        #endregion

    }

    public class EnemyAttackParameter
    {
        public int AttackAmount { get; private set; }
        public float AttackDelaySec { get; private set; }

        public float AttackRange { get; private set; }

        public EnemyAttackParameter(int attackAmount, float attackDelaySec, float attackRange)
        {
            this.AttackAmount = attackAmount;
            this.AttackDelaySec = attackDelaySec;
            this.AttackRange = attackRange;
        }
    }
}