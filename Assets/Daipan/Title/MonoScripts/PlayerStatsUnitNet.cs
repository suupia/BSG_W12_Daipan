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
    [SerializeField] Image readyImage = null!;
    [SerializeField] Sprite OK = null!;
    [SerializeField] Sprite NG = null!;
    [SerializeField] TextMeshProUGUI readyText = null!;
    [SerializeField] CustomButton playerRoleButton = null!;

    [SerializeField] Image playerRoleButtom = null!;

    //[SerializeField] TextMeshProUGUI playerRoleText = null!;
    [SerializeField] Sprite streamerUI = null!;
    [SerializeField] Sprite antiUI = null!;

    [Networked] public PlayerRef NetworkedPlayerRef { get; set; }

    [Networked]
    [OnChangedRender(nameof(OnPlayerNameChanged))]
    public NetworkString<_32> PlayerName { get; set; }

    [Networked]
    [OnChangedRender(nameof(OnPlayerRoleChanged))]
    public PlayerRoleEnum PlayerRole { get; private set; } 

    [Networked]
    [OnChangedRender(nameof(OnIsReadyChanged))]
    public NetworkBool IsReady { get; set; }

    TitleMonoOnline _titleMonoOnline = null!;

    void Awake()
    {
        // viewObject.SetActive(false);
    }

    public override void Spawned()
    {
        base.Spawned();
        _titleMonoOnline = FindObjectOfType<TitleMonoOnline>();
        if (_titleMonoOnline == null)
        {
            Debug.LogWarning($"TitleMonoNew is null. Active scene: {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}");
            return;
        }

        transform.SetParent(_titleMonoOnline.playerStatsUnitParent, false);

        youAreThisImage.color = HasStateAuthority ? new Color(79f / 255f, 235f / 255f, 230f / 255f) : new Color(253f / 255f, 120f / 255f, 134f / 255f);
        playerRoleButton.onClick += () =>
        {
            if (HasStateAuthority)
                PlayerRole = PlayerRole switch
                {
                    PlayerRoleEnum.Streamer => PlayerRoleEnum.Anti,
                    PlayerRoleEnum.Anti => PlayerRoleEnum.Streamer,
                    _ => PlayerRoleEnum.Streamer
                };
        };

        if(PlayerRole == PlayerRoleEnum.None) PlayerRole = PlayerRoleEnum.Streamer; // 同期されていなければStreamerにする 

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
        playerRoleButtom.sprite = PlayerRole switch
        {
            PlayerRoleEnum.Streamer => streamerUI,
            PlayerRoleEnum.Anti => antiUI,
            _ => null
        };
    }

    void OnIsReadyChanged()
    {
        readyText.text = IsReady ? "OK" : "NG";
        readyImage.sprite = IsReady ? OK : NG;
        _titleMonoOnline.CheckAllReady();
    }
}

public enum PlayerRoleEnum
{
    None,
    Streamer,
    Anti
}