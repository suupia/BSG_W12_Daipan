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
        [SerializeField] CustomButton button1 = null!;
        [SerializeField] CustomButton button2 = null!;
        [SerializeField] CustomButton button3 = null!;

        private AntiEnemySpawnerNetwork _antiEnemySpawnerNetwork;

        [Inject]
        public void Initialize(
            NetworkRunner runner,
            PlayerDataTransporterNetWrapper playerDataTransporterNetWrapper,
            AntiEnemySpawnerNetwork antiEnemySpawnerNetwork
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
        }

        void Start()
        {
            button1.onClick += () => _antiEnemySpawnerNetwork.SpawnEnemy(EnemyEnum.Red); // todo : 敵を出現させる 
            button2.onClick += () => Debug.Log("Button2"); //
            button3.onClick += () => Debug.Log("Button3"); // 
        }


    }
}

