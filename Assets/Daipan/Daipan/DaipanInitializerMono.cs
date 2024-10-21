#nullable enable

using System;
using System.Collections.Generic;
using Daipan.Battle.scripts;
using Daipan.Core;
using Daipan.Daipan;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DaipanInitializerMono : MonoBehaviour, INetworkRunnerCallbacks
{
    /// <summary>Callback is invoked when a Scene Load has finished</summary>
    /// <param name="runner">NetworkRunner reference</param>
    public void OnSceneLoadDone(NetworkRunner runner)
    {
        Debug.Log($"[INetworkRunnerCallbacks] OnSceneLoadDone: {runner}");
        Debug.Log($"Active Scene: {SceneManager.GetActiveScene().name}");
        // if (SceneManager.GetActiveScene().name == SceneName.DaipanSceneNet.ToString())
        // {
        //     Debug.Log($"Scene Loaded: {SceneName.DaipanSceneNet}");
        //
        //     var daipanScopeNet = FindObjectOfType<DaipanScopeNet>();
        //
        //     var dtoNet = FindObjectOfType<DTONet>();
        //     Debug.Log($"dtoNet: {dtoNet}");
        //
        //     daipanScopeNet.Runner = runner;
        //
        //     daipanScopeNet.Build();
        // }
    }

    #region Callbacks

    /// <summary>
    /// Callback from a <see cref="T:Fusion.NetworkRunner" /> when a new <see cref="T:Fusion.NetworkObject" /> has exit the Area of Interest
    /// </summary>
    /// <param name="runner">NetworkRunner reference</param>
    /// <param name="obj">NetworkObject reference</param>
    /// <param name="player">PlayerRef reference</param>
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    /// <summary>
    /// Callback from a <see cref="T:Fusion.NetworkRunner" /> when a new <see cref="T:Fusion.NetworkObject" /> has entered the Area of Interest
    /// </summary>
    /// <param name="runner">NetworkRunner reference</param>
    /// <param name="obj">NetworkObject reference</param>
    /// <param name="player">PlayerRef reference</param>
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    /// <summary>
    /// Callback from a <see cref="T:Fusion.NetworkRunner" /> when a new player has joined.
    /// </summary>
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
    }

    /// <summary>
    /// Callback from a <see cref="T:Fusion.NetworkRunner" /> when a player has disconnected.
    /// </summary>
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
    }

    /// <summary>
    /// Callback from <see cref="T:Fusion.NetworkRunner" /> that polls for user inputs.
    /// The <see cref="T:Fusion.NetworkInput" /> that is supplied expects:
    /// <code>
    /// input.Set(new CustomINetworkInput() { /* your values */ });
    /// </code>
    /// </summary>
    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
    }

    /// <summary>
    /// Callback from <see cref="T:Fusion.NetworkRunner" /> when an input is missing.
    /// </summary>
    /// <param name="runner">NetworkRunner reference</param>
    /// <param name="player">PlayerRef reference which the input is missing from</param>
    /// <param name="input">NetworkInput reference which is missing</param>
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    /// <summary>Called when the runner is shutdown</summary>
    /// <param name="runner">The runner being shutdown</param>
    /// <param name="shutdownReason">Describes the reason Fusion was Shutdown</param>
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    /// <summary>
    /// Callback when <see cref="T:Fusion.NetworkRunner" /> successfully connects to a server or host.
    /// </summary>
    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    /// <summary>
    /// Callback when <see cref="T:Fusion.NetworkRunner" /> disconnects from a server or host.
    /// </summary>
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
    }

    /// <summary>
    /// Callback when <see cref="T:Fusion.NetworkRunner" /> receives a Connection Request from a Remote Client
    /// </summary>
    /// <param name="runner">Local NetworkRunner</param>
    /// <param name="request">Request information</param>
    /// <param name="token">Request Token</param>
    public void OnConnectRequest(
        NetworkRunner runner,
        NetworkRunnerCallbackArgs.ConnectRequest request,
        byte[] token)
    {
    }

    /// <summary>
    /// Callback when <see cref="T:Fusion.NetworkRunner" /> fails to connect to a server or host.
    /// </summary>
    public void OnConnectFailed(
        NetworkRunner runner,
        NetAddress remoteAddress,
        NetConnectFailedReason reason)
    {
    }

    /// <summary>
    /// This callback is invoked when a manually dispatched simulation message is received from a remote peer
    /// </summary>
    /// <param name="runner">The runner this message is for</param>
    /// <param name="message">The message pointer</param>
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    /// <summary>
    /// This callback is invoked when a new List of Sessions is received from Photon Cloud
    /// </summary>
    /// <param name="runner">The runner this object exists on</param>
    /// <param name="sessionList">Updated list of Session</param>
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    /// <summary>
    /// Callback is invoked when the Authentication procedure returns a response from the Authentication Server
    /// </summary>
    /// <param name="runner">The runner this object exists on</param>
    /// <param name="data">Custom Authentication Reply Values</param>
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    /// <summary>
    /// Callback is invoked when the Host Migration process has started
    /// </summary>
    /// <param name="runner">The runner this object exists on</param>
    /// <param name="hostMigrationToken">Migration Token that stores all necessary information to restart the Fusion Runner</param>
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    /// <summary>
    /// Callback is invoked when a Reliable Data Stream has been received
    /// </summary>
    /// <param name="runner">NetworkRunner reference</param>
    /// <param name="player">Which PlayerRef the stream was sent from</param>
    /// <param name="key">ReliableKey reference that identifies the data stream</param>
    /// <param name="data">Data received</param>
    public void OnReliableDataReceived(
        NetworkRunner runner,
        PlayerRef player,
        ReliableKey key,
        ArraySegment<byte> data)
    {
    }

    /// <summary>
    /// Callback is invoked when a Reliable Data Stream is being received, reporting its progress
    /// </summary>
    /// <param name="runner">NetworkRunner reference</param>
    /// <param name="player">Which PlayerRef the stream is being sent from</param>
    /// <param name="key">ReliableKey reference that identifies the data stream</param>
    /// <param name="progress">Progress of the stream</param>
    public void OnReliableDataProgress(
        NetworkRunner runner,
        PlayerRef player,
        ReliableKey key,
        float progress)
    {
    }


    /// <summary>Callback is invoked when a Scene Load has started</summary>
    /// <param name="runner">NetworkRunner reference</param>
    public void OnSceneLoadStart(NetworkRunner runner)
    {
        Debug.Log($"[INetworkRunnerCallbacks] OnSceneLoadStart: {runner}");
        Debug.Log($"Active Scene: {SceneManager.GetActiveScene().name}");
    }

    #endregion
}