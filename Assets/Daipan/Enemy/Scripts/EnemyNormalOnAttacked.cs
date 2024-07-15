#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;

namespace Daipan.Enemy.Scripts
{
    public class EnemyNormalOnAttacked : IEnemyOnAttacked
    {
        EnemyNormalViewMono? _enemyNormalViewMono;
        public void SetView(AbstractEnemyViewMono enemyViewMono)
        {
            // [Precondition]
            if (enemyViewMono is EnemyNormalViewMono normalViewMono)
                _enemyNormalViewMono = normalViewMono;
        }
        public Hp OnAttacked(Hp hp, IPlayerParamData playerParamData)
        {
           return new Hp (hp.Value - playerParamData.GetAttack()); 
        } 
    } 
}

