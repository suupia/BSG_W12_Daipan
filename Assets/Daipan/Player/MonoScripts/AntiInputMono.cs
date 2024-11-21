#nullable enable
using System;
using Daipan.Transporter;
using Fusion;
using UnityEngine;
using VContainer;
using Daipan.AntiNet.Scripts;
using Daipan.Enemy.Scripts;

namespace Daipan.Player.MonoScripts
{
    public class AntiInputMono : MonoBehaviour
    {
        [SerializeField] GameObject viewObject = null!;
        [SerializeField] CustomButton yellowNormalButton = null!;
        [SerializeField] CustomButton redNormalButton = null!;
        [SerializeField] CustomButton blueNormalButton = null!;
        [SerializeField] CustomButton yellowBossButton = null!;
        [SerializeField] CustomButton redBossButton = null!;
        [SerializeField] CustomButton blueBossButton = null!;
        [SerializeField] CustomButton antiCommentButton = null!;

        private AntiEnemySpawnerNetwork _antiEnemySpawnerNetwork = null!;
        private AntiCommentSpawnerNetwork _antiCommentSpawnerNetwork = null!;

        [Inject]
        public void Initialize(
            NetworkRunner runner,
            PlayerDataTransporterNetWrapper playerDataTransporterNetWrapper,
            AntiEnemySpawnerNetwork antiEnemySpawnerNetwork,
            AntiCommentSpawnerNetwork antiCommentSpawnerNetwork
        )
        {
            Debug.Log($"AntiInputMono Initialize isAnti: {playerDataTransporterNetWrapper.GetPlayerRoleEnum(runner.LocalPlayer) == PlayerRoleEnum.Anti}");
            var isAnti = playerDataTransporterNetWrapper.GetPlayerRoleEnum(runner.LocalPlayer) == PlayerRoleEnum.Anti;
            viewObject.SetActive(isAnti);


            foreach (var player in runner.ActivePlayers)
            {
                Debug.Log($"GetPlayerRoleEnum({player}): {playerDataTransporterNetWrapper.GetPlayerRoleEnum(player)}");
            }

            _antiEnemySpawnerNetwork = antiEnemySpawnerNetwork;
            _antiCommentSpawnerNetwork = antiCommentSpawnerNetwork;
        }

        void Start()
        {
            yellowNormalButton.onClick += () => _antiEnemySpawnerNetwork.SpawnEnemy(EnemyEnum.Yellow);
            redNormalButton.onClick += () => _antiEnemySpawnerNetwork.SpawnEnemy(EnemyEnum.Red);
            blueNormalButton.onClick += () => _antiEnemySpawnerNetwork.SpawnEnemy(EnemyEnum.Blue);
            yellowBossButton.onClick += () => _antiEnemySpawnerNetwork.SpawnEnemy(EnemyEnum.YellowBoss);
            redBossButton.onClick += () => _antiEnemySpawnerNetwork.SpawnEnemy(EnemyEnum.RedBoss);
            blueBossButton.onClick += () => _antiEnemySpawnerNetwork.SpawnEnemy(EnemyEnum.BlueBoss);
            antiCommentButton.onClick += () => _antiCommentSpawnerNetwork.SpawnAntiComment("アンチだ！！");
        }


    }
}

