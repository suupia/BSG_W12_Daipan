#nullable enable
using System.Collections.Generic;
using Daipan.Enemy.Scripts;
using VContainer;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyParamDataBuilder
    {
        public EnemyParamDataBuilder(
            IContainerBuilder builder,
            EnemyParamManager enemyParamManager
            )
        {
            var enemyParams = new List<EnemyParamData>();
            foreach (var enemyParam in enemyParamManager.enemyParams)
            {
                enemyParams.Add(new EnemyParamData()
                {
                    EnemyEnum = () => enemyParam.GetEnemyEnum,
                    GetAttackAmount = () => enemyParam.enemyAttackParam.attackAmount,
                    GetAttackDelayDec = () => enemyParam.enemyAttackParam.attackDelaySec,
                    GetAttackRange = () => enemyParam.enemyAttackParam.attackRange,
                    GetCurrentHp = () => enemyParam.enemyHpParam.hpAmount
                }); 
            }
            var enemyParamContainer = new EnemyParamDataContainer(enemyParams);
            builder.RegisterInstance(enemyParamContainer);
        }
    } 
}

