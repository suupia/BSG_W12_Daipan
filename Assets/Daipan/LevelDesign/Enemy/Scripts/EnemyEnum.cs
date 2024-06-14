#nullable enable
namespace Daipan.LevelDesign.Enemy.Scripts
{
    public enum EnemyEnum
    {
        None,
        Red,
        Blue,
        Yellow,
        [IsBoss(true)] RedBoss,
        [IsBoss(true)] BlueBoss,
        [IsBoss(true)] YellowBoss,
    }
}