using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace Daipan.Transporter.Scripts
{
    /// <summary>
    /// ロビーシーンからゲームシーンに遷移する際に、PlayerRefとPlayerDataの組を保持するクラス
    /// 他にも保持したい状態があったらこのクラスに追加する
    /// </summary>
    public class PlayerDataTransporter
    {
        public int PlayerCount => _playerDataDictionary.Count;
        readonly Dictionary<PlayerRef, PlayerData> _playerDataDictionary = new();

        public void AddPlayerRef(PlayerRef playerRef)
        {
            Debug.Log($"Registering playerRef:{playerRef} as {_playerDataDictionary.Count + 1}P");
            _playerDataDictionary[playerRef] = new PlayerData();
        }

        public PlayerRoleEnum GetPlayerRoleEnum(PlayerRef playerRef)
        {
            if (_playerDataDictionary.TryGetValue(playerRef, out PlayerData playerData))
            {
                Debug.Log($"GetPlayerRoleEnum playerRef:{playerRef} is {playerData.Role}");
                return playerData.Role;
            }

            return PlayerRoleEnum.None;
        }
        
        public string GetPlayerName(PlayerRef playerRef)
        {
            if (_playerDataDictionary.TryGetValue(playerRef, out PlayerData playerData))
            {
                Debug.Log($"GetPlayerName playerRef:{playerRef} is {playerData.Name}");
                return playerData.Name.Value;
            }

            return string.Empty;
        }

        public void SetPlayerData(PlayerRef playerRef, PlayerData playerData)
        {
            Debug.Log($"Registering playerRef:{playerRef} as {playerData}");
            _playerDataDictionary[playerRef] = playerData;
        }
    }

    public struct PlayerData : INetworkStruct
    {
        public NetworkString<_32> Name;
        public PlayerRoleEnum Role;
    }
}