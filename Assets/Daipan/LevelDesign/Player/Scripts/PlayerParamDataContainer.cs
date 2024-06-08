#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using UnityEngine;

namespace Daipan.LevelDesign.Player.Scripts
{
    public class PlayerParamDataContainer
    {
        readonly IEnumerable<PlayerParamData> _playerParamDataList;
        public PlayerParamDataContainer(IEnumerable<PlayerParamData> playerParamDataList)
        {
            _playerParamDataList = playerParamDataList;
        }
        public PlayerParamData GetPlayerParamData(PlayerColor playerEnum)
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