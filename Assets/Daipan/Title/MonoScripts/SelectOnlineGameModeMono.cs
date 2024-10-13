#nullable enable
using Cysharp.Threading.Tasks;
using Daipan.Battle.scripts;
using Fusion;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectOnlineGameModeMono : MonoBehaviour
{
    [SerializeField] GameObject selectModePopup = null!;
    [SerializeField] CustomButton joinAsHostButton = null!;
    [SerializeField] CustomButton joinAsClientButton = null!;
    [SerializeField] TMP_InputField roomNameInputField = null!;
    [SerializeField] CustomButton backButton = null!;
    
    string RoomName => roomNameInputField.text;

    void Start()
    {
        selectModePopup.SetActive(false);

        joinAsClientButton.OnClick += () => StartGame(RoomName, GameMode.Client).Forget();
        
        //GameMode.Hostとして扱うかは未定。仮でAutoHostOrClientに設定
        //もし、GameMode.Hostかつ同じルーム名で始めた場合はStartGameExceptionがthrowされる
        joinAsHostButton.OnClick += () => StartGame(RoomName, GameMode.AutoHostOrClient).Forget();


        backButton.OnClick += () => selectModePopup.SetActive(false);
    }

    void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; //ゲームプレイ終了
#else
        Application.Quit();//ゲームプレイ終了
#endif
    }
    
    public static async UniTaskVoid StartGame(string roomName, GameMode gameMode)
    {
        // if (_isStarted) return;

        // var runnerManager = FindObjectOfType<NetworkRunnerManager>();
        // Assert.IsNotNull(runnerManager, "NetworkRunnerManagerをシーンに配置してください");
        //
        // _isStarted = true; // 連続で呼ばれるのを防ぐ
        //
        // await runnerManager.AttemptStartScene(roomName, gameMode);
        //
        // runnerManager.Runner.Spawn(carryInitializerPrefab);
        //
        // Debug.Log("Transitioning to LobbySceneTestRoom");
        // SceneTransition.TransitionSceneWithNetworkRunner(runnerManager.Runner, SceneName.LobbyScene);
    }


}