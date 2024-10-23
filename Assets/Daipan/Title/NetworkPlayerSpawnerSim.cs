using Fusion;
using System.Collections;
using System.Collections.Generic;
using Daipan.Transporter.Scripts;
using UnityEngine;
using VContainer;

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
            playerStatsUnitNet.PlayerRef = playerRef;

            var titleMonoNew = FindObjectOfType<TitleMonoNew>();
            if (titleMonoNew != null) playerStatsUnitNet.PlayerName = titleMonoNew.LocalPlayerName;

            var rootScope = FindObjectOfType<RootScope>();
            var playerDataTransporter = rootScope.Container.Resolve<PlayerDataTransporterNet>();
            playerDataTransporter.AddPlayerRef(playerRef);
        }
    }

    public void PlayerLeft(PlayerRef player)
    {
        // todo : 実装する
        // if (PlayerStatsUnitNet.LocalPlayer != null)
        //     PlayerStatsUnitNet.LocalPlayer.IsMasterClient = Runner.IsSharedModeMasterClient;
    }
}