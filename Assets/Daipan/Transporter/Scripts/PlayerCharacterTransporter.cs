using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace Daipan.Transporter.Scripts
{
    /// <summary>
    /// ロビーシーンからゲームシーンに遷移する際に、PlayerRefとPlayerDataの組を保持するクラス
    /// 他にも保持したい状態があったらこのクラスに追加する
    /// </summary>
    public class PlayerRoleEnumTransporter
    {
        public int PlayerCount => _playerDataDictionary.Count;
        readonly Dictionary<PlayerRef, PlayerData> _playerDataDictionary = new();

        public void AddPlayerNumber(PlayerRef playerRef, NetworkString<_32> name)
        {
            Debug.Log($"Registering playerRef:{playerRef} as {_playerDataDictionary.Count + 1}P");
            _playerDataDictionary[playerRef] = new PlayerData(){Name = name, Role = PlayerRoleEnum.None}; 
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

        public void SetRole(PlayerRef playerRef, PlayerData playerData)
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