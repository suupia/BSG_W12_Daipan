using Fusion;
using System.Collections;
using System.Collections.Generic;
using Daipan.Transporter.Scripts;
using UnityEngine;
using VContainer;
using Daipan.Transporter;

/// <summary>
/// Basic player spawn based on the main shared mode sample.
/// </summary>
public class NetworkPlayerSpawnerSim : SimulationBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] PlayerStatsUnitNet playerStatsUnitNetPrefab;

    public void PlayerJoined(PlayerRef playerRef)
    {
        Debug.Log("PlayerJoined!!");
        if (playerRef == Runner.LocalPlayer)
        {
            var playerStatsUnitNet = Runner.Spawn(playerStatsUnitNetPrefab, new Vector3(0, 1, 0), Quaternion.identity);
            playerStatsUnitNet.NetworkedPlayerRef = playerRef;

            var titleMonoNew = FindObjectOfType<TitleMonoOnline>();
            if (titleMonoNew != null) playerStatsUnitNet.PlayerName = titleMonoNew.LocalPlayerName;

            // var rootScope = FindObjectOfType<RootScope>();
            // var playerDataTransporter = rootScope.Container.Resolve<PlayerDataTransporterNetWrapper>();
            // Debug.Log($"Is PlayerDataTransporter NULL{playerDataTransporter == null}");
            // playerDataTransporter.AddPlayerRef(playerRef);
        }
    }

    public void PlayerLeft(PlayerRef player)
    {
        // todo : 実装する
        // if (PlayerStatsUnitNet.LocalPlayer != null)
        //     PlayerStatsUnitNet.LocalPlayer.IsMasterClient = Runner.IsSharedModeMasterClient;
    }
}