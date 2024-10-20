#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Fusion;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

public class AttackEffectNetTestInitializer : MonoBehaviour
{
    [SerializeField] CustomButton startSharedButton = null!;
    [SerializeField] TextMeshProUGUI debugText = null!;
    [SerializeField] NetworkRunner networkRunnerPrefab = null!;

    [SerializeField] PlayerParamManager playerParamManager = null!;
    [SerializeField] PlayerAttackEffectNet playerAttackEffectNetPrefab = null!;
    [SerializeField] PlayerAttackEffectMono playerAttackEffectMonoPrefab = null!;

    // 完全テスト用のシンプルなプレハブ
    [SerializeField] PlayerAttackEffectNetTest playerAttackEffectNetTestPrefab = null!;

    NetworkRunner? _runner;
    PlayerAttackEffectNet? _playerAttackEffectNet;
    PlayerAttackEffectMono? _playerAttackEffectMono;
    PlayerAttackEffectNetTest? _playerAttackEffectNetTest;

    void Awake()
    {
        startSharedButton.OnClick += StartSharedButtonClicked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_runner == null) return;
            if (!_runner.IsSharedModeMasterClient) return;

            if (_playerAttackEffectNet != null) _playerAttackEffectNet?.Despawned(_runner, false);
            _playerAttackEffectNet = _runner.Spawn(playerAttackEffectNetPrefab, onBeforeSpawned: (runner, obj) =>
            {
                var attackEffectMono = obj.GetComponent<PlayerAttackEffectNet>();
                var playerParam = playerParamManager.playerParams.First(); // 適当に1つ目を取得
                var playerParamData = new PlayerParamData(playerParam);
                attackEffectMono?.SetUp(PlayerColor.Blue, () => null);
            });
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (_runner == null) return;
            if (!_runner.IsSharedModeMasterClient) return;

            if (_playerAttackEffectMono != null) Destroy(_playerAttackEffectMono);
            _playerAttackEffectMono = Instantiate(playerAttackEffectMonoPrefab);
            var playerParam = playerParamManager.playerParams.First(); // 適当に1つ目を取得
            var playerParamData = new PlayerParamData(playerParam);
            // _playerAttackEffectMono?.Initialize(playerParamData); // Containerを入れないといけない
            _playerAttackEffectMono?.SetUp(PlayerColor.Blue, () => null);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (_runner == null) return;
            if (!_runner.IsSharedModeMasterClient) return;

            if (_playerAttackEffectNetTest != null) _playerAttackEffectNetTest?.Despawned(_runner, false);
            _playerAttackEffectNetTest = _runner.Spawn(playerAttackEffectNetTestPrefab);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (_runner == null) return;
            if (!_runner.IsSharedModeMasterClient) return;

            _playerAttackEffectNet?.Defenced();
            _playerAttackEffectMono?.Defenced();
            
            Debug.Log($"_playerAttackEffectNetTest HasStateAuthority : {_playerAttackEffectNetTest?.HasStateAuthority}");
            _playerAttackEffectNetTest?.Hit(() => { });
        }
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (_runner == null) return;
            if (!_runner.IsSharedModeMasterClient) return;
            
            Debug.Log($"_playerAttackEffectNetTest HasStateAuthority : {_playerAttackEffectNetTest?.HasStateAuthority}");
            _playerAttackEffectNetTest?.HitLocal(() => { });
        }
        
        

        if (_runner == null)
            debugText.text = "Runner is null";
        else if (_runner.IsCloudReady)
            debugText.text = "Cloud is ready";
        else
            debugText.text = "---";
    }

    void StartSharedButtonClicked()
    {
        _runner = Instantiate(networkRunnerPrefab);
        _runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = "test",
            Scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex)
            // SceneManager = FindObjectOfType<NetworkSceneManagerDefault>(),
        });
    }
}