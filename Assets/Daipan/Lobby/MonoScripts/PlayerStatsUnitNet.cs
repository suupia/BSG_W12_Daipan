#nullable enable

using System;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUnitNet : NetworkBehaviour
{
    [SerializeField] GameObject viewObject = null!;
    [SerializeField] Image youAreThisImage = null!;
    [SerializeField] TextMeshProUGUI playerName = null!;
    [SerializeField] TextMeshProUGUI ready = null!;
    [SerializeField] CustomButton roleButton = null!;

    public PlayerRef PlayerRef { get; set; }

    [Networked]
    [OnChangedRender(nameof(OnPlayerNameChanged))]
    public NetworkString<_16> PlayerName { get; set; }

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

        // The OnRenderChanged functions are called during spawn to make sure they are set properly for players who have already joined the room.
        OnPlayerNameChanged();
    }

    public void Show()
    {
        viewObject.SetActive(true);
    }

    void OnPlayerNameChanged()
    {
        playerName.text = PlayerName.Value;
    }
}