#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using UnityEngine;

namespace Daipan.Player.LevelDesign.Scripts
{
    public sealed class PlayerParamDataContainer : IPlayerParamDataContainer
    {
        readonly IEnumerable<PlayerParamData> _playerParamDataList;
        public PlayerParamDataContainer(PlayerParamManager playerParamManager)
        {
            _playerParamDataList = playerParamManager.playerParams
                .Select(playerParam => new PlayerParamData(playerParam));
        }
        public IPlayerParamData GetPlayerParamData(PlayerColor playerEnum)
        { 
            // debug
            foreach (var playerParamData in _playerParamDataList)
            {
                Debug.Log($"playerParamData.PlayerEnum() = {playerParamData.PlayerEnum()}");
            }

            var data = _playerParamDataList.FirstOrDefault(x => x.PlayerEnum() == playerEnum);
            Debug.Log($"PlayerColor = {playerEnum}");
            if (data == null)
            {
                throw new System.Exception("PlayerParamData not found");
            }
            return data;
        }
    }
}