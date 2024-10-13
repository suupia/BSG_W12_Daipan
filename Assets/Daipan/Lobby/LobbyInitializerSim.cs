using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;
using VContainer;

public class LobbyInitializerSim : SimulationBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] NetworkObject playerStatsUnitParent;
    NetworkPlayerStatsUnitSpawner _networkPlayerStatsUnitSpawner;
    public bool IsInitialized { get; private set; }

    [SerializeField] NetworkPrefabRef playerStatsUnitPrefab; // todo : 一旦ここにおく

    [Inject]
    public void Construct(
        NetworkPlayerStatsUnitSpawner networkPlayerStatsUnitSpawner 
    )
    {
         _networkPlayerStatsUnitSpawner = networkPlayerStatsUnitSpawner;
         
    }


    async void Start()
    {

    }

    void IPlayerJoined.PlayerJoined(PlayerRef player)
    {
        if (Runner.IsServer)
        {
            Runner.Spawn(playerStatsUnitPrefab, inputAuthority:player);
        } 
        // Todo: RunnerがSetActiveシーンでシーンの切り替えをする時に対応するシーンマネジャーのUniTaskのキャンセルトークンを呼びたい
    }


    void IPlayerLeft.PlayerLeft(PlayerRef player)
    {
        if (Runner.IsServer) Runner.Spawn(playerStatsUnitPrefab, parentId: playerStatsUnitParent.Id);
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
        NetworkSpawnFlags flags = (NetworkSpawnFlags) 0,
        NetworkId parentId = default
        )
    {   
        var networkObject = runner.Spawn(prefabRef, position, rotation, inputAuthority, onBeforeSpawned, flags);
         RPC_SetParent(runner, networkObject.Id, parentId);
        return networkObject;
    }

    [Rpc]
    static void RPC_SetParent(NetworkRunner runner, NetworkId childId, NetworkId parentId, RpcInfo info = default)
    {
        // 親として設定するオブジェクトを NetworkId で取得
        runner.TryFindObject(parentId, out var parentObject); 

        if (parentObject == null)
        {
            Debug.LogWarning($"Parent object with NetworkId {parentId} not found.");
            return;
        }
        
        // 現在の NetworkObject にアクセスし、親を設定
        runner.TryFindObject(childId, out var childObject); 
        
        if(childObject == null)
        {
            Debug.LogWarning($"Child object with NetworkId {childId} not found.");
            return;
        }

        childObject.transform.SetParent(parentObject.transform);
 
    }
}