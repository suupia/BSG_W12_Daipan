#nullable enable
using Cysharp.Threading.Tasks;
using Daipan.Battle.scripts;
using Daipan.NetworkUtility;
using Fusion;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assert = UnityEngine.Assertions.Assert;

public class SelectOnlineGameModeMono : MonoBehaviour
{
    [SerializeField] GameObject selectModePopup = null!;
    [SerializeField] CustomButton startSharedButton = null!;
    [SerializeField] TMP_InputField roomNameInputField = null!;
    [SerializeField] CustomButton backButton = null!;

    string RoomName => roomNameInputField.text;
    static bool _isStarted; // 連続で呼ばれるのを防ぐ

    void Awake()
    {
        selectModePopup.SetActive(false);

        //GameMode.Hostとして扱うかは未定。仮でAutoHostOrClientに設定
        //もし、GameMode.Hostかつ同じルーム名で始めた場合はStartGameExceptionがthrowされる
        startSharedButton.OnClick += () =>
        {
            StartGame(RoomName, GameMode.Shared).Forget();
        };
        backButton.OnClick += () => selectModePopup.SetActive(false);
    }

    public void ShowPopup()
    {
        selectModePopup.SetActive(true);
    }


    static async UniTaskVoid StartGame(string roomName, GameMode gameMode)
    {
        if (_isStarted) return;

        _isStarted = true;

        var runner = FindObjectOfType<NetworkRunner>();
        Assert.IsNotNull(runner, "NetworkRunnerをシーンに配置してください");

        await runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            SessionName = roomName,
            Scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex)
            // SceneManager = FindObjectOfType<NetworkSceneManagerDefault>(),
        });
    }
}