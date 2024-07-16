#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;

namespace Daipan.Enemy.Scripts
{
    public class EnemyNormalOnAttacked : IEnemyOnAttacked
    {
        public Hp OnAttacked(Hp hp, IPlayerParamData playerParamData)
        {
           return new Hp (hp.Value - playerParamData.GetAttack()); 
        } 
    } 
}

