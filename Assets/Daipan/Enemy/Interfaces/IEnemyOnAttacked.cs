#nullable enable
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyOnAttacked
    {
        public void OnAttacked(Hp hp, IPlayerParamData playerParamData);
    } 
}

