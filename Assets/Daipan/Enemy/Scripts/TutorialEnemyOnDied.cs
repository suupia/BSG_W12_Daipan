#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;

namespace Daipan.Enemy.Scripts
{
    public class TutorialEnemyOnDied : IEnemyOnDied
    {
        readonly EnemyMono _enemyMono;
        public TutorialEnemyOnDied(EnemyMono enemyMono)
        {
            _enemyMono = enemyMono;
        }
        public void OnDied()
        {
            // todo : 倒された時に、その敵の種類に応じて、チュートリアルのIsSuccessをtrueにする
            
        }
    } 
}

