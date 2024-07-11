#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Player.Scripts;

namespace Daipan.Player.Interfaces
{
    public interface IPlayerOnAttacked
    {
        Hp OnAttacked(Hp hp, IEnemyParamData enemyParamData);
    }
}