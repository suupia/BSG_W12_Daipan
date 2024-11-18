using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace Daipan.Transporter.Scripts
{
    /// <summary>
    /// ロビーシーンからゲームシーンに遷移する際に、PlayerRefとPlayerDataの組を保持するクラス
    /// 他にも保持したい状態があったらこのクラスに追加する
    /// </summary>
    public class PlayerDataTransporterNet : NetworkBehaviour
    {
        public int PlayerCount => PlayerDataDictionary.Count;
        [SerializeField][Networked] NetworkDictionary<PlayerRef, PlayerData> PlayerDataDictionary => default;

        public void AddPlayerRef(PlayerRef playerRef)
        {
            Debug.Log($"Registering playerRef:{playerRef} as {PlayerDataDictionary.Count + 1}P");
            // PlayerDataDictionary.Add(playerRef, new PlayerData());
        }

        public PlayerRoleEnum GetPlayerRoleEnum(PlayerRef playerRef)
        {
            if (PlayerDataDictionary.TryGet(playerRef, out PlayerData playerData))
            {
                Debug.Log($"GetPlayerRoleEnum playerRef:{playerRef} is {playerData.Role}");
                return playerData.Role;
            }
            Debug.LogWarning($"GetPlayerRoleEnum playerRef:{playerRef} is not found");
            return PlayerRoleEnum.None;
        }

        public string GetPlayerName(PlayerRef playerRef)
        {
            if (PlayerDataDictionary.TryGet(playerRef, out PlayerData playerData))
            {
                Debug.Log($"GetPlayerName playerRef:{playerRef} is {playerData.Name}");
                return playerData.Name.Value;
            }

            return string.Empty;
        }

        public void SetPlayerData(PlayerRef playerRef, PlayerData playerData)
        {
            Debug.Log($"Registering playerRef:{playerRef} as {playerData.Name},{playerData.Role}");
            PlayerDataDictionary.Set(playerRef, playerData);
        }
    }

    [Serializable]
    public struct PlayerData : INetworkStruct
    {
        public NetworkString<_32> Name;
        public PlayerRoleEnum Role;
    }
}