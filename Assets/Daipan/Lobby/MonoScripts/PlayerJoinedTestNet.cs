#nullable enable

using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerJoinedTestNet : NetworkBehaviour , IPlayerJoined
{
    void IPlayerJoined.PlayerJoined(PlayerRef player)
    {
        Debug.Log($"PlayerJoined Player : {player},  Scene : {SceneManager.GetActiveScene().name}");
        
    } 
    
    
}