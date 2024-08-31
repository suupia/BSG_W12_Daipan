#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Player.MonoScripts;
using Daipan.Tutorial.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public class TutorialEnemyOnDied : IEnemyOnDied
    {
        readonly EnemyMono _enemyMono;
        readonly TutorialCurrentStep _tutorialCurrentStep;

        public TutorialEnemyOnDied(EnemyMono enemyMono, TutorialCurrentStep tutorialCurrentStep)
        {
            _enemyMono = enemyMono;
            _tutorialCurrentStep = tutorialCurrentStep;
        }

        public void OnDied()
        {
            // todo : 倒された時に、その敵の種類に応じて、チュートリアルのIsSuccessをtrueにする

            Debug.Log($"EnemyType: {_enemyMono.EnemyEnum}を攻撃");
            if (_enemyMono.EnemyEnum == EnemyEnum.Blue)
            {
                if (_tutorialCurrentStep.CurrentStep is BlueEnemyTutorial redEnemyTutorial)
                {
                    // Blueの敵を一体倒すチュートリアル
                    redEnemyTutorial.SetSuccess();
                }
            }

            if (_enemyMono.EnemyEnum == EnemyEnum.Red)
            {
                if (_tutorialCurrentStep.CurrentStep is SequentialEnemyTutorial sequentialEnemyTutorial)
                {
                    // 本来は全ての敵を倒したかどうかを判定するべきだが、最後の敵がたまたまRedなので、これで判定する
                    sequentialEnemyTutorial.SetSuccess();
                }
            }

            if (_enemyMono.EnemyEnum == EnemyEnum.Totem2)
            {
                if (_tutorialCurrentStep.CurrentStep is TotemEnemyTutorial totemEnemyTutorial)
                {
                    totemEnemyTutorial.SetSuccess();
                }
            }
        }
    }
}