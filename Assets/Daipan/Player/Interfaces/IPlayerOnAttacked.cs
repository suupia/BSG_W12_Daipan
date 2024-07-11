#nullable enable
using System.Collections.Generic;
using Daipan.Enemy.Interfaces;
using Daipan.Player.Scripts;

namespace Daipan.Player.Interfaces
{
    public interface IPlayerOnAttacked
    {
        void SetPlayerViews(List<AbstractPlayerViewMono?> playerViewMonos);
        Hp OnAttacked(Hp hp, IEnemyParamData enemyParamData);
    }
}