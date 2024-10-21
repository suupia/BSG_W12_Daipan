#nullable enable
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUnitNet : NetworkBehaviour
{
    [SerializeField] GameObject viewObject = null!;
    [SerializeField] Image youAreThisImage = null!;
    [SerializeField] TextMeshProUGUI playerNameText = null!;
    [SerializeField] TextMeshProUGUI readyText = null!;
    [SerializeField] CustomButton playerRoleButton = null!;
    [SerializeField] TextMeshProUGUI playerRoleText = null!;

    public PlayerRef PlayerRef { get; set; }

    [Networked]
    [OnChangedRender(nameof(OnPlayerNameChanged))]
    public NetworkString<_16> PlayerName { get; set; }

    [Networked]
    [OnChangedRender(nameof(OnPlayerRoleChanged))]
    PlayerRoleEnum PlayerRole { get; set; } = PlayerRoleEnum.Streamer;

    [Networked]
    [OnChangedRender(nameof(OnIsReadyChanged))]
    public NetworkBool IsReady { get; set; }

    TitleMonoNew _titleMonoNew = null!;

    void Awake()
    {
        // viewObject.SetActive(false);
    }

    public override void Spawned()
    {
        base.Spawned();
        _titleMonoNew = FindObjectOfType<TitleMonoNew>();
        if (_titleMonoNew == null)
        {
            Debug.LogWarning($"TitleMonoNew is null. Active scene: {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}");
            return;
        }

        transform.SetParent(_titleMonoNew.playerStatsUnitParent, false);

        youAreThisImage.gameObject.SetActive(HasStateAuthority);
        playerRoleButton.OnClick += () =>
        {
            if (HasStateAuthority)
                PlayerRole = PlayerRole switch
                {
                    PlayerRoleEnum.Streamer => PlayerRoleEnum.Anti,
                    PlayerRoleEnum.Anti => PlayerRoleEnum.Streamer,
                    _ => PlayerRoleEnum.Streamer
                };
        };

        // The OnRenderChanged functions are called during spawn to make sure they are set properly for players who have already joined the room.
        OnPlayerNameChanged();
        OnPlayerRoleChanged();
        OnIsReadyChanged();
    }

    public void Show()
    {
        viewObject.SetActive(true);
    }
    
    // OnChangedRender functions

    void OnPlayerNameChanged()
    {
        playerNameText.text = PlayerName.Value;
    }

    void OnPlayerRoleChanged()
    {
        playerRoleText.text = PlayerRole switch
        {
            PlayerRoleEnum.Streamer => "Streamer",
            PlayerRoleEnum.Anti => "Anti",
            _ => "None"
        };
    }

    void OnIsReadyChanged()
    {
        readyText.text = IsReady ? "OK" : "NG";
        _titleMonoNew.CheckAllReady();
    }
}

public enum PlayerRoleEnum
{
    None,
    Streamer,
    Anti
}