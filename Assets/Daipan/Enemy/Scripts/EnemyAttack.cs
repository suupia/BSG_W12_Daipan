#nullable enable
using Daipan.Battle.interfaces;
using Daipan.LevelDesign.Enemy.Scripts;

namespace Daipan.Enemy.Scripts
{
    public class EnemyAttack
    {
        readonly EnemyParamData _enemyParamData;
        public EnemyAttack(EnemyParamData enemyParamData)
        {
            _enemyParamData = enemyParamData;
        }
        
        public void Attack(IHpSetter hpSetter)
        {
            hpSetter.CurrentHp -= _enemyParamData.GetAttackAmount();
        }
    } 
}

