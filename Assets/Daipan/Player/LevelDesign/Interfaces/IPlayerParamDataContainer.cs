#nullable enable
using Daipan.Player.MonoScripts;

namespace Daipan.Player.LevelDesign.Interfaces
{
    public interface IPlayerParamDataContainer
    {
        IPlayerParamData GetPlayerParamData(PlayerColor playerColor);
    } 
}

