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

    void Awake()
    {
        viewObject.SetActive(false);
    }

    public void Show()
    {
        viewObject.SetActive(true);
    }
}