#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.Enemy.MonoScripts
{
    public class EnemyViewMono : AbstractEnemyViewMono
    {
        [SerializeField] EnemyNormalViewMono enemyNormalViewMono = null!;
        [SerializeField] EnemyBossViewMono enemyBossViewMono = null!;
        AbstractEnemyViewMono _selectedEnemyViewMono = null!;
        public override void SetDomain(IEnemyViewParamData enemyParamData)
        {
            if (enemyParamData.GetEnemyEnum().IsBoss() == true)
            {
                   enemyNormalViewMono.gameObject.SetActive(false);
                   enemyBossViewMono.gameObject.SetActive(true);
                   _selectedEnemyViewMono = enemyBossViewMono;
                   _selectedEnemyViewMono.SetDomain(enemyParamData);
            }
            else
            {
                enemyNormalViewMono.gameObject.SetActive(true);
                enemyBossViewMono.gameObject.SetActive(false);
                _selectedEnemyViewMono = enemyNormalViewMono;
                _selectedEnemyViewMono.SetDomain(enemyParamData);
            }
        }
        
        public override void SetHpGauge(double currentHp, int maxHp)
        {
            _selectedEnemyViewMono.SetHpGauge(currentHp, maxHp);
        }
        
        public override void Move()
        {
            _selectedEnemyViewMono.Move();
        }
        
        public override void Attack()
        {
            _selectedEnemyViewMono.Attack();
        }
        
        public override void Died(System.Action onDied)
        {
            _selectedEnemyViewMono.Died(onDied);
        }
        
        public override void Daipaned(System.Action onDaipaned)
        {
            _selectedEnemyViewMono.Daipaned(onDaipaned);
        }
        
        public override void Highlight(bool isHighlighted)
        {
            _selectedEnemyViewMono.Highlight(isHighlighted);
        }
    } 
}

