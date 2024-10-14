#nullable enable
using Cysharp.Threading.Tasks;
using Daipan.Battle.scripts;
using Daipan.NetworkUtility;
using Fusion;
using TMPro;
using UnityEditor;
using UnityEngine;
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
        startSharedButton.OnClick += () => StartGame(RoomName, GameMode.Shared).Forget();
        backButton.OnClick += () => selectModePopup.SetActive(false);
        
    }

    public void ShowPopup()
    {
        selectModePopup.SetActive(true);
    }

    
    public static async UniTaskVoid StartGame(string roomName, GameMode gameMode)
    {
        if (_isStarted) return;

        var runnerManager = FindObjectOfType<NetworkRunnerManager>();
        Assert.IsNotNull(runnerManager, "NetworkRunnerManagerをシーンに配置してください");
        
        _isStarted = true; 
        
        await runnerManager.AttemptStartScene(roomName, gameMode);
        
        
        Debug.Log("Transitioning to LobbySceneTestRoom");
        SceneTransition.TransitionSceneWithNetworkRunner(runnerManager.Runner, SceneName.Lobby);
    }


}