#nullable enable
using TMPro;
using UnityEngine;

public class TitleMonoNew : MonoBehaviour
{
    [SerializeField] GameObject joinPanel = null!;
    [SerializeField] GameObject playerStatsPanel = null!;
    
    // JoinPanel
    [SerializeField] TMP_InputField localPlayerNameInputField = null!;
    [SerializeField] TMP_InputField remoteNameInputField = null!;
    [SerializeField] CustomButton closeJoinPanelButton = null!;
    
    // PlayerStatsPanel
    [SerializeField] Transform playerStatsUnitParent = null!;
    
    void Awake()
    {
        joinPanel.SetActive(false);
        playerStatsPanel.SetActive(false);
    }
    
    
}