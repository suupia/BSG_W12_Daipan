#nullable enable
namespace Daipan.Player.LevelDesign.Interfaces
{
    public interface IPlayerHpParamData
    {
        int GetCurrentHp();
        int SetCurrentHp(int value);
        int GetAntiCommentThreshold();
    }
 
}
