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
using UnityEngine.UI;
using System;

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

        [SerializeField] CustomButton yellowButton = null!;
        [SerializeField] CustomButton redButton = null!;
        [SerializeField] CustomButton blueButton = null!;
        [SerializeField] CustomButton yellowBossButton = null!;
        [SerializeField] CustomButton redBossButton = null!;
        [SerializeField] CustomButton blueBossButton = null!;

        [Inject]
        public void Initialize(
            AntiStateValue stateValue
            , IEnemySpawnedCostParam enemySpawnedCostParam
            , SpawnEnemyCostValue spawnEnemyCostValue)
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

            Observable
                .EveryValueChanged(spawnEnemyCostValue, x => x.Value)
                .Subscribe(_ =>
                {
                    UpdateButtonState(yellowButton, EnemyEnum.Yellow, stateValue.AntiStateEnum, enemySpawnedCostParam, spawnEnemyCostValue);
                    UpdateButtonState(redButton, EnemyEnum.Red, stateValue.AntiStateEnum, enemySpawnedCostParam, spawnEnemyCostValue);
                    UpdateButtonState(blueButton, EnemyEnum.Blue, stateValue.AntiStateEnum, enemySpawnedCostParam, spawnEnemyCostValue);
                    UpdateButtonState(yellowBossButton, EnemyEnum.YellowBoss, stateValue.AntiStateEnum, enemySpawnedCostParam, spawnEnemyCostValue);
                    UpdateButtonState(redBossButton, EnemyEnum.RedBoss, stateValue.AntiStateEnum, enemySpawnedCostParam, spawnEnemyCostValue);
                    UpdateButtonState(blueBossButton, EnemyEnum.BlueBoss, stateValue.AntiStateEnum, enemySpawnedCostParam, spawnEnemyCostValue);
                })
                .AddTo(this);
        }
        void ChangeValue(
            TMP_Text text
            , EnemyEnum enemyEnum
            , AntiStateEnum antiStateEnum
            , IEnemySpawnedCostParam enemySpawnedCostParam)
        {
            int cost = GetCost(enemyEnum, antiStateEnum, enemySpawnedCostParam);
            text.text = $"{cost}";
        }

        void UpdateButtonState(
            CustomButton button
            , EnemyEnum enemyEnum
            , AntiStateEnum antiStateEnum
            , IEnemySpawnedCostParam enemySpawnedCostParam
            , SpawnEnemyCostValue spawnEnemyCostValue
        )
        {
            int cost = GetCost(enemyEnum, antiStateEnum, enemySpawnedCostParam);
            if (spawnEnemyCostValue.Value < cost)
            {
                Debug.Log($"{enemyEnum}Button is Judged false");
                button.GetComponent<Button>().interactable = false;
            }
            else
            {
                Debug.Log($"{enemyEnum}Button is Judged true");
                button.GetComponent<Button>().interactable = true;
            }
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