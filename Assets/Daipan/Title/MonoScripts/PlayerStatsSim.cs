#nullable enable

using System;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;
using VContainer;

public class PlayerStatsSim : SimulationBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] PlayerStatsCanvasNet playerStatsCanvasPrefab= null!;
    NetworkPlayerStatsUnitSpawner _networkPlayerStatsUnitSpawner;
    public bool IsInitialized { get; private set; }

    async void Start()
    {
        // [Precondition]
        var runner = FindObjectOfType<NetworkRunner>();
        if (runner == null)
        {
            Debug.LogError("NetworkRunner is not found");
            return;
        }

        // NetworkRunner が起動するまで待機する
        await UniTask.WaitUntil(() => runner.IsRunning);

        runner.AddGlobal(this);
        Debug.Log($"Runner: {runner}");

        // Debug.Log($"Object : {Object}, Runner.Object: {runner.Object}");
        //
        // Debug.Log($"playerStatsUnitParent.InputAuthority == Runner.LocalPlayer: {playerStatsUnitParent.InputAuthority == Runner.LocalPlayer}");
        //
        // {
        //     Debug.Log($"Runner.IsRunning : {Runner.IsRunning}, PlayerStatsUnitParent.Id: {playerStatsUnitParent.Id}");
        //     Runner.Spawn(playerStatsUnitPrefab, parentId: playerStatsUnitParent.Id);
        // }
    }




    void IPlayerJoined.PlayerJoined(PlayerRef player)
    {
        Debug.Log("PlayerJoined in  LobbyInitializerSim");
        Debug.Log($"Runner.IsRunning : {Runner.IsRunning}");
        Debug.Log($"playerStatsCanvasPrefab: {playerStatsCanvasPrefab}");
        if (player == Runner.LocalPlayer) Runner.Spawn(playerStatsCanvasPrefab, inputAuthority: player);
    }


    void IPlayerLeft.PlayerLeft(PlayerRef player)
    {
    }
}

public static class NetworkSpawnerExtension
{
    public static NetworkObject Spawn(
        this NetworkRunner runner,
        NetworkPrefabRef prefabRef,
        Vector3? position = null,
        Quaternion? rotation = null,
        PlayerRef? inputAuthority = null,
        NetworkRunner.OnBeforeSpawned onBeforeSpawned = null,
        NetworkSpawnFlags flags = (NetworkSpawnFlags)0,
        NetworkId parentId = default
    )
    {
        Debug.Log($"Spawn {prefabRef} with parent {parentId}");
        var networkObject = runner.Spawn(prefabRef, position, rotation, inputAuthority, onBeforeSpawned, flags);
        RPC_SetParent(runner, networkObject.Id, parentId);
        return networkObject;
    }

    [Rpc]
    static void RPC_SetParent(NetworkRunner runner, NetworkId childId, NetworkId parentId, RpcInfo info = default)
    {
        if (runner.IsClient) return;

        // 親として設定するオブジェクトを NetworkId で取得
        runner.TryFindObject(parentId, out var parentObject);

        if (parentObject == null)
        {
            Debug.LogWarning($"Parent object with NetworkId {parentId} not found.");
            return;
        }

        // 現在の NetworkObject にアクセスし、親を設定
        runner.TryFindObject(childId, out var childObject);

        if (childObject == null)
        {
            Debug.LogWarning($"Child object with NetworkId {childId} not found.");
            return;
        }

        Debug.Log($"Set parent {parentObject.Id} to child {childObject.Id}");
        childObject.transform.SetParent(parentObject.transform);
    }
}