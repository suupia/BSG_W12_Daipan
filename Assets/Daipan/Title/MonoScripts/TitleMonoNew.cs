#nullable enable
using System.Linq;
using Daipan.Battle.scripts;
using Fusion;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TitleMonoNew : MonoBehaviour
{
    [SerializeField] CanvasGroup titleCanvasGroup = null!;
    [SerializeField] NetworkRunner networkRunnerPrefab = null!;

    // JoinPanel
    [SerializeField] GameObject joinPanel = null!;
    [SerializeField] TMP_InputField localPlayerNameInputField = null!;
    [SerializeField] TMP_InputField localRoomNameInputField = null!;
    [SerializeField] CustomButton joinRoomButton = null!;
    [SerializeField] CustomButton closeJoinPanelButton = null!;

    // PlayerStatsPanel
    [SerializeField] GameObject playerStatsPanel = null!;
    [SerializeField] TextMeshProUGUI roomName = null!;
    [SerializeField] public Transform playerStatsUnitParent = null!;
    [SerializeField] CustomButton readyButton = null!;
    [SerializeField] CustomButton startGameButton = null!; // MasterClient only

    // ErrorPanel
    [SerializeField] GameObject errorMessagePanel = null!;
    [SerializeField] TextMeshProUGUI errorMessageText = null!;


    void Awake()
    {
        joinPanel.SetActive(false);
        playerStatsPanel.SetActive(false);
        errorMessagePanel.SetActive(false);
        // JoinPanel
        joinRoomButton.OnClick += JoinButtonOnClicked;
        // PlayerStatsPanel
        readyButton.OnClick += ReadyButtonClicked;
        startGameButton.gameObject.SetActive(false);
        startGameButton.OnClick += StartGameButtonClicked;
    }

    async void JoinButtonOnClicked()
    {
        titleCanvasGroup.interactable = false;

        var startGameArgs = new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = localRoomNameInputField.text,
            PlayerCount = 20
        };

        var runner = Instantiate(networkRunnerPrefab);

        var result = await runner.StartGame(startGameArgs);

        if (result.Ok)
        {
            roomName.text = "Room:  " + runner.SessionInfo.Name;
            GoToPlayerStatsPanel();
        }
        else
        {
            roomName.text = string.Empty;

            GoToJoinPanel();

            errorMessagePanel.SetActive(true);
            errorMessageText.text = result.ErrorMessage;

            Debug.LogWarning(result.ErrorMessage);
        }

        titleCanvasGroup.interactable = true;
    }

    public void GoToJoinPanel()
    {
        joinPanel.SetActive(true);
        playerStatsPanel.SetActive(false);
    }

    void GoToPlayerStatsPanel()
    {
        joinPanel.SetActive(false);
        playerStatsPanel.SetActive(true);
    }

    void ReadyButtonClicked()
    {
        var playerStatsUnits = playerStatsUnitParent.GetComponentsInChildren<PlayerStatsUnitNet>();
        foreach (var playerStatsUnit in playerStatsUnits)
            if (playerStatsUnit.HasStateAuthority)
                playerStatsUnit.IsReady = !playerStatsUnit.IsReady;
    }

    void StartGameButtonClicked()
    {
        // todo : transit to DaipanNet scene
        var runner = FindObjectOfType<NetworkRunner>();
        SceneTransition.TransitionSceneWithNetworkRunner(runner, SceneName.DaipanScene);
    }

    public void CheckAllReady()
    {
        var playerStatsUnits = playerStatsUnitParent.GetComponentsInChildren<PlayerStatsUnitNet>();
        var isAllReady = playerStatsUnits.All(playerStatsUnit => playerStatsUnit.IsReady)
                         && playerStatsUnits.Length > 1;
        var runner = FindObjectOfType<NetworkRunner>();
        if (runner.IsSharedModeMasterClient) startGameButton.gameObject.SetActive(isAllReady);
    }

    public string LocalPlayerName => localPlayerNameInputField.text;
}