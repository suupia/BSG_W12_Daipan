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
using Daipan.AntiNet.MonoScripts;
using UnityEngine.EventSystems;

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
        private AntiCommentInputViewMono _antiCommentInputViewMono = null!;

        [Inject]
        public void Initialize(
            NetworkRunner runner,
            PlayerDataTransporterNetWrapper playerDataTransporterNetWrapper,
            AntiEnemySpawnerNetwork antiEnemySpawnerNetwork,
            AntiCommentSpawnerNetwork antiCommentSpawnerNetwork,
            AntiStateValue antiStateValue,
            AntiCommentInputViewMono commentInputViewMono
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
            _antiCommentInputViewMono = commentInputViewMono;
        }

        void Start()
        {
            yellowNormalButton.onClick += () => _antiEnemySpawnerNetwork.SpawnEnemy(EnemyEnum.Yellow);
            redNormalButton.onClick += () => _antiEnemySpawnerNetwork.SpawnEnemy(EnemyEnum.Red);
            blueNormalButton.onClick += () => _antiEnemySpawnerNetwork.SpawnEnemy(EnemyEnum.Blue);
            yellowBossButton.onClick += () => _antiEnemySpawnerNetwork.SpawnEnemy(EnemyEnum.YellowBoss);
            redBossButton.onClick += () => _antiEnemySpawnerNetwork.SpawnEnemy(EnemyEnum.RedBoss);
            blueBossButton.onClick += () => _antiEnemySpawnerNetwork.SpawnEnemy(EnemyEnum.BlueBoss);

            // 入力開始
            antiCommentInput.onSelect.AddListener(_ =>
            {
                _antiCommentInputViewMono.OpenChat();
            });
            // 入力更新
            antiCommentInput.onValueChanged.AddListener(_ =>
            {
                _antiCommentInputViewMono.UpdateCharacters(antiCommentInput.text);
            });
            // 入力終了
            antiCommentInput.onEndEdit.AddListener(_ =>
            {
                _antiCommentInputViewMono.CloseChat();
                _antiCommentSpawnerNetwork.SpawnAntiComment(antiCommentInput.text);
                antiCommentInput.text = "";

                // if (EventSystem.current.currentSelectedGameObject != null)
                // {
                //     EventSystem.current.SetSelectedGameObject(null);
                // }
                // antiCommentInput.DeactivateInputField(false);
            });
            // Enterを押して入力終了
            antiCommentInput.onSubmit.AddListener(_ =>
            {
                if (EventSystem.current.currentSelectedGameObject != null)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                }
            });
            antiCommentInput.onDeselect.AddListener(_ =>
            {
                Debug.Log("DeSelect");
            });
        }


    }
}

