#nullable enable
using System;
using Daipan.Transporter;
using Fusion;
using UnityEngine;
using VContainer;
using Daipan.AntiNet.Scripts;
using Daipan.Enemy.Scripts;
using UnityEngine.UI;
using TMPro;
using R3;

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
        [SerializeField] TMP_InputField antiCommentInput = null!;

        private AntiEnemySpawnerNetwork _antiEnemySpawnerNetwork = null!;
        private AntiCommentSpawnerNetwork _antiCommentSpawnerNetwork = null!;

        [Inject]
        public void Initialize(
            NetworkRunner runner,
            PlayerDataTransporterNetWrapper playerDataTransporterNetWrapper,
            AntiEnemySpawnerNetwork antiEnemySpawnerNetwork,
            AntiCommentSpawnerNetwork antiCommentSpawnerNetwork,
            AntiStateValue antiStateValue
        )
        {
            Debug.Log($"AntiInputMono Initialize isAnti: {playerDataTransporterNetWrapper.GetPlayerRoleEnum(runner.LocalPlayer) == PlayerRoleEnum.Anti}");
            var isAnti = playerDataTransporterNetWrapper.GetPlayerRoleEnum(runner.LocalPlayer) == PlayerRoleEnum.Anti;
            viewObject.SetActive(isAnti);

            Observable.EveryValueChanged(antiStateValue, x => x.AntiStateEnum)
            .Subscribe(value =>
            {
                if (value == AntiStateEnum.BAN) antiCommentInput.interactable = false;
                else antiCommentInput.interactable = true;
            }).AddTo(this);

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
            antiCommentInput.onEndEdit.AddListener(_ =>
            {
                _antiCommentSpawnerNetwork.SpawnAntiComment(antiCommentInput.text);
                antiCommentInput.text = "";
            });
        }


    }
}

