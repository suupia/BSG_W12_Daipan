#nullable enable
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyOnAttacked
    {
        public void SetView(AbstractEnemyViewMono? enemyViewMono);
        public Hp OnAttacked(Hp hp, IPlayerParamData playerParamData);
    } 
}

