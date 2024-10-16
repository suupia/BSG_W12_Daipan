#nullable enable
using Fusion;
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
    
    // ErrorPanel
    [SerializeField] GameObject errorMessagePanel = null!;
    [SerializeField] TextMeshProUGUI errorMessageText = null!;
    

    void Awake()
    {
        joinPanel.SetActive(false);
        playerStatsPanel.SetActive(false);
        errorMessagePanel.SetActive(false);
        
        joinRoomButton.OnClick += StartGame;
    }
    
    async void StartGame()
    {
        titleCanvasGroup.interactable = false;

        StartGameArgs startGameArgs = new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = localRoomNameInputField.text,
            PlayerCount = 20,
        };

        var runner = Instantiate(networkRunnerPrefab);

        StartGameResult result = await runner.StartGame(startGameArgs);

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


    
    
}