#nullable enable

using System;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUnitNet : NetworkBehaviour
{
    [SerializeField] GameObject viewObject = null!;
    [SerializeField] Image youAreThis = null!;
    [SerializeField] TextMeshProUGUI playerName = null!;
    [SerializeField] TextMeshProUGUI playerRole = null!;
    [SerializeField] TextMeshProUGUI ready = null!;
    
    public string PlayerName
    {
        get => playerName.text;
        set => playerName.text = value;
    }
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
    }

    public void Show()
    {
        viewObject.SetActive(true);
    }
}