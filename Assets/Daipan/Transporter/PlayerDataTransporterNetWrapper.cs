#nullable enable
using UnityEngine;
using Daipan.Transporter.Scripts;
using Fusion;

namespace Daipan.Transporter;

public class PlayerDataTransporterNetWrapper
{
    PlayerDataTransporterNet? _dataTransporterNet;
    PlayerDataTransporterNet DataTransporterNet => _dataTransporterNet ??= Object.FindObjectOfType<PlayerDataTransporterNet>();

    public void AddPlayerRef(PlayerRef playerRef)
    {
        DataTransporterNet?.AddPlayerRef(playerRef);
    }

    public PlayerRoleEnum GetPlayerRoleEnum(PlayerRef playerRef)
    {
        return DataTransporterNet?.GetPlayerRoleEnum(playerRef) ?? PlayerRoleEnum.None;
    }

    public string GetPlayerName(PlayerRef playerRef)
    {
        return DataTransporterNet?.GetPlayerName(playerRef) ?? string.Empty;
    }

    public void SetPlayerData(PlayerRef playerRef, PlayerData playerData)
    {
        DataTransporterNet.SetPlayerData(playerRef, playerData);
    }
}