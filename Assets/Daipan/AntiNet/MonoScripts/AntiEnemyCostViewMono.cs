#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.AntiNet.Scripts;
using TMPro;
using UnityEngine;
using VContainer;
using R3;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Net;

namespace Daipan.AntiNet.MonoScripts
{
    public class AntiEnemyCostViewMono : MonoBehaviour
    {
        [SerializeField] TMP_Text yellowCostText = null!;
        [SerializeField] TMP_Text redCostText = null!;
        [SerializeField] TMP_Text blueCostText = null!;
        [SerializeField] TMP_Text yellowBossCostText = null!;
        [SerializeField] TMP_Text redBossCostText = null!;
        [SerializeField] TMP_Text blueBossCostText = null!;


        [Inject]
        public void Initialize(
            AntiStateValue stateValue
            , AntiStateValue antiStateValue
            , IEnemySpawnedCostParam enemySpawnedCostParam)
        {
            Observable
                .EveryValueChanged(stateValue, x => x.AntiStateEnum)
                .Subscribe(value =>
                {
                    ChangeValue(yellowCostText, EnemyEnum.Yellow, value, enemySpawnedCostParam);
                    ChangeValue(redCostText, EnemyEnum.Red, value, enemySpawnedCostParam);
                    ChangeValue(blueCostText, EnemyEnum.Blue, value, enemySpawnedCostParam);
                    ChangeValue(yellowBossCostText, EnemyEnum.YellowBoss, value, enemySpawnedCostParam);
                    ChangeValue(redBossCostText, EnemyEnum.RedBoss, value, enemySpawnedCostParam);
                    ChangeValue(blueBossCostText, EnemyEnum.BlueBoss, value, enemySpawnedCostParam);
                })
                .AddTo(this);
        }
        void ChangeValue(TMP_Text text, EnemyEnum enemyEnum, AntiStateEnum antiStateEnum, IEnemySpawnedCostParam enemySpawnedCostParam)
        {
            int cost = GetCost(enemyEnum, antiStateEnum, enemySpawnedCostParam);
            text.text = $"{cost}";
        }

        int GetCost(EnemyEnum enemyEnum, AntiStateEnum antiStateEnum, IEnemySpawnedCostParam enemySpawnedCostParam)
        {
            int multiplier = 1;
            if (antiStateEnum == AntiStateEnum.BAN) multiplier *= 2;

            if (enemyEnum.IsBoss() == true) return enemySpawnedCostParam.BossEnemyCost * multiplier;
            return enemySpawnedCostParam.NormalEnemyCost * multiplier;
        }
    }
}