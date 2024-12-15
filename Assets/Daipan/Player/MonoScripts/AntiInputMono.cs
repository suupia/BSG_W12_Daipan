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

        AntiEnemySpawnerNetwork _antiEnemySpawnerNetwork = null!;
        AntiCommentSpawnerNetwork _antiCommentSpawnerNetwork = null!;
        AntiCommentInputViewMono _antiCommentInputViewMono = null!;
        AntiStateValue _antiStateValue = null!;
        bool _isAnti;
        bool _isInitialized;

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
            _isAnti = isAnti;

            Observable.EveryValueChanged(antiStateValue, x => x.AntiStateEnum)
                .Subscribe(value =>
                {
                    if (value == AntiStateEnum.BAN) antiCommentInput.interactable = false;
                    else antiCommentInput.interactable = true;
                }).AddTo(this);

            foreach (var player in runner.ActivePlayers) Debug.Log($"GetPlayerRoleEnum({player}): {playerDataTransporterNetWrapper.GetPlayerRoleEnum(player)}");

            _antiEnemySpawnerNetwork = antiEnemySpawnerNetwork;
            _antiCommentSpawnerNetwork = antiCommentSpawnerNetwork;
            _antiCommentInputViewMono = commentInputViewMono;
            _antiStateValue = antiStateValue;
            _isInitialized = true;
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
                Debug.Log($"[AntiInputMono] OnSelect");
                _antiCommentInputViewMono.OpenChat();
            });
            // 入力更新
            antiCommentInput.onValueChanged.AddListener(_ =>
            {
                Debug.Log($"[AntiInputMono] OnValueChanged");
                _antiCommentInputViewMono.UpdateCharacters(antiCommentInput.text);
            });
            // 入力終了
            antiCommentInput.onEndEdit.AddListener(_ =>
            {
                Debug.Log($"[AntiInputMono] OnEndEdit");
                _antiCommentInputViewMono.CloseChat();
                _antiCommentSpawnerNetwork.SpawnAntiComment(antiCommentInput.text);
                antiCommentInput.text = "";
            });
            // Enterを押して入力終了
            antiCommentInput.onSubmit.AddListener(_ =>
            {
                Debug.Log($"[AntiInputMono] OnSubmit");
                if (EventSystem.current.currentSelectedGameObject != null)
                {
                    Debug.Log("SetSelectedGameObject(null)");
                    EventSystem.current.SetSelectedGameObject(null);
                }
            });
            antiCommentInput.onDeselect.AddListener(_ => { Debug.Log("DeSelect"); });
        }

        void Update()
        {
            if (!_isInitialized) return;
            if (!_isAnti) return;
            if (Input.GetKeyDown(KeyCode.Alpha1)) _antiEnemySpawnerNetwork.SpawnEnemy(EnemyEnum.Yellow);
            if (Input.GetKeyDown(KeyCode.Alpha2)) _antiEnemySpawnerNetwork.SpawnEnemy(EnemyEnum.Red);
            if (Input.GetKeyDown(KeyCode.Alpha3)) _antiEnemySpawnerNetwork.SpawnEnemy(EnemyEnum.Blue);
            if (Input.GetKeyDown(KeyCode.Alpha4)) _antiEnemySpawnerNetwork.SpawnEnemy(EnemyEnum.YellowBoss);
            if (Input.GetKeyDown(KeyCode.Alpha5)) _antiEnemySpawnerNetwork.SpawnEnemy(EnemyEnum.RedBoss);
            if (Input.GetKeyDown(KeyCode.Alpha6)) _antiEnemySpawnerNetwork.SpawnEnemy(EnemyEnum.BlueBoss);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(_antiStateValue.AntiStateEnum == AntiStateEnum.BAN) return;
                if (_antiCommentInputViewMono.IsChatOpen) return;

                // 既にInputFieldが選択されていなければ、Enterキーで選択状態にしてキーボードからの入力を開始可能にする
                if (EventSystem.current.currentSelectedGameObject != antiCommentInput.gameObject)
                {
                    // イベントシステムで該当のInputFieldを選択オブジェクトとして設定
                    EventSystem.current.SetSelectedGameObject(antiCommentInput.gameObject);
                    // InputFieldのSelectメソッドでフォーカスを当てる
                    antiCommentInput.Select();
                }
                else
                {
                    // フォーカスがある場合の処理はない
                }
            }

            if (_antiCommentInputViewMono.IsChatOpen && _antiStateValue.AntiStateEnum == AntiStateEnum.BAN)
            {
                // フォーカスを外す
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }
}