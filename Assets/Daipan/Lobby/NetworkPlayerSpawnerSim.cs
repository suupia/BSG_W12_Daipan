using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic player spawn based on the main shared mode sample.
/// </summary>
public class NetworkPlayerSpawnerSim : SimulationBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] PlayerStatsUnitNet playerStatsUnitNetPrefab;

    public void PlayerJoined(PlayerRef playerRef)
    {
        if (playerRef == Runner.LocalPlayer)
        {
            var playerStatsUnitNet = Runner.Spawn(playerStatsUnitNetPrefab, new Vector3(0, 1, 0), Quaternion.identity);
            // todo : ここで、Refを設定
        }

        FusionConnector.Instance?.OnPlayerJoin(Runner);
    }

    public void PlayerLeft(PlayerRef player)
    {
        // todo : 実装する
        // if (PlayerStatsUnitNet.LocalPlayer != null)
        //     PlayerStatsUnitNet.LocalPlayer.IsMasterClient = Runner.IsSharedModeMasterClient;
    }
}
