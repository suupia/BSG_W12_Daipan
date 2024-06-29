#nullable enable
using Daipan.Battle.interfaces;
using Daipan.Enemy.MonoScripts;


namespace Daipan.Enemy.Scripts
{
    public class TotemEnemyHp : EnemyHp
    {
        public TotemEnemyHp(int maxHp, EnemyMono enemyMono, EnemyCluster enemyCluster)
            : base(maxHp, enemyMono, enemyCluster) { }


        public override void DecreaseHp(EnemyDamageArgs enemyDamageArgs)
        {
            base.DecreaseHp(enemyDamageArgs);
        }
    }
}