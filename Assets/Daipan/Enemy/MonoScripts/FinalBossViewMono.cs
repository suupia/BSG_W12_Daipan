#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Enemy.MonoScripts
{
    public sealed class FinalBossViewMono : MonoBehaviour 
    {
        FinalBossViewMono _selectedEnemyViewMono = null!;

        public void SetDomain(IFinalBossViewParamData enemyParamData)
        {
            Debug.Log("SetDomain FinalBossViewMono");
            // Debug.Log("SetDomain enemy enum: " + enemyParamData.GetEnemyEnum());
            // SwitchEnemyView(enemyParamData);
            _selectedEnemyViewMono.SetDomain(enemyParamData);
        }
        // void SwitchEnemyView(EnemyEnum enemyEnum)
        // {
        //     
        // }
        public void SetHpGauge(double currentHp, int maxHp)
        {
            _selectedEnemyViewMono.SetHpGauge(currentHp, maxHp);
        }

        public void Move()
        {
            _selectedEnemyViewMono.Move();
        }

        public void Attack()
        {
            _selectedEnemyViewMono.Attack();
        }

        public void SummonEnemy()
        {
           
        }
        public void Died(System.Action onDied)
        {
            _selectedEnemyViewMono.Died(onDied);
        }

        public void Daipaned(System.Action onDaipaned)
        {
            _selectedEnemyViewMono.Daipaned(onDaipaned);
        }

        public void Highlight(bool isHighlighted)
        {
            _selectedEnemyViewMono.Highlight(isHighlighted);
        }
    } 
}

