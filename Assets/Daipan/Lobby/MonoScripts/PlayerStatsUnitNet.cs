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

    void Awake()
    {
        // viewObject.SetActive(false);
    }

    public override void Spawned()
    {
        base.Spawned();
        var titleMonoNew = FindObjectOfType<TitleMonoNew>();
        if (titleMonoNew == null)
        {
            Debug.LogError($"TitleMonoNew is null");
            return;
        }

        transform.SetParent(titleMonoNew.playerStatsUnitParent, false);

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
    }
}

public enum PlayerRoleEnum
{
    None,
    Streamer,
    Anti
}