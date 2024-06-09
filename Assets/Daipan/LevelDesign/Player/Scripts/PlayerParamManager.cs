#nullable enable
using System.Collections.Generic;
using UnityEngine;

namespace Daipan.LevelDesign.Player.Scripts
{
    [CreateAssetMenu(fileName = "PlayerParamManager", menuName = "ScriptableObjects/Player/PlayerParamManager",
        order = 1)]
    public sealed class PlayerParamManager : ScriptableObject
    {
        public PlayerHpParam playerHpParam = null!;
        public List<PlayerParam> playerParams = null!;
    }
}