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
        NetworkRunner runner
        , NetworkPlayerStatsUnitSpawner networkPlayerStatsUnitSpawner
    )
    {
        runner.AddGlobal(this);
        _networkPlayerStatsUnitSpawner = networkPlayerStatsUnitSpawner;
        Debug.Log($"Runner: {runner}");

        Debug.Log($"playerStatsUnitParent.InputAuthority == Runner.LocalPlayer: {playerStatsUnitParent.InputAuthority == Runner.LocalPlayer}");

        {
            Debug.Log($"Runner.IsRunning : {Runner.IsRunning}");
            Runner.Spawn(playerStatsUnitPrefab, parentId: playerStatsUnitParent.Id);
        }
    }


    async void Start()
    {
    }

    void IPlayerJoined.PlayerJoined(PlayerRef player)
    {
        Debug.Log("PlayerJoined in  LobbyInitializerSim");
        Debug.Log($"Runner.IsRunning : {Runner.IsRunning}");
        if (Runner.IsServer) Runner.Spawn(playerStatsUnitPrefab, inputAuthority: player);
        // Todo: RunnerがSetActiveシーンでシーンの切り替えをする時に対応するシーンマネジャーのUniTaskのキャンセルトークンを呼びたい
    }


    void IPlayerLeft.PlayerLeft(PlayerRef player)
    {
        if (Runner.IsServer) Runner.Spawn(playerStatsUnitPrefab, parentId: playerStatsUnitParent.Id);
    }
}