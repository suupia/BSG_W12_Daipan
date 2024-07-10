#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;

namespace Daipan.Enemy.Scripts
{
    public class EnemyNormalOnAttacked : IEnemyOnAttacked
    {
        public void OnAttacked(Hp hp, IPlayerParamData playerParamData)
        {
            hp.Decrease(playerParamData.GetAttack());
        } 
    } 
}

