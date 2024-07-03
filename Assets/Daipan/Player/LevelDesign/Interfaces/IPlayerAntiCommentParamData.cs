#nullable enable
namespace Daipan.Player.LevelDesign.Interfaces
{
   
    public interface IPlayerAntiCommentParamData
    {
        public int GetAntiCommentThreshold();
        public double GetAntiCommentPercentOnMissAttacks(int index); 
    } 
}
