#nullable enable
namespace Daipan.Player.LevelDesign.Interfaces
{
   
    public interface IPlayerAntiCommentParamData
    {
        public int GetAntiCommentThreshold();
        public int GetMissedAttackCountForAntiComment();
        public double GetAntiCommentPercentOnMissAttacks(int index); 
    } 
}
