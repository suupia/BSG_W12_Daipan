#nullable enable

using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUnitNet : NetworkBehaviour
{
    [SerializeField] Image youAreThis = null!;
    [SerializeField] TextMeshProUGUI playerName = null!;
    [SerializeField] TextMeshProUGUI playerRole = null!;
    [SerializeField] TextMeshProUGUI ready = null!;
}