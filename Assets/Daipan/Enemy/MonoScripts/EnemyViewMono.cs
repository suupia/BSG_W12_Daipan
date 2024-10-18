#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.Enemy.MonoScripts
{
    public sealed class EnemyViewMono : AbstractEnemyViewMono, IGetAbstractEnemyViewMono
    {
        public AbstractEnemyViewMono GetAbstractEnemyViewMono() => _selectedEnemyViewMono;
        [SerializeField] EnemyNormalViewMono enemyNormalViewMono = null!;
        [SerializeField] EnemyBoss1ViewMono enemyBoss1ViewMono = null!; // Tank
        [SerializeField] EnemyBoss2ViewMono enemyBoss2ViewMono = null!; // 筋肉
        [SerializeField] EnemyBoss3ViewMono enemyBoss3ViewMono = null!; // 素早い
        [SerializeField] EnemySpecialViewMono enemySpecialViewMono = null!;
        [SerializeField] EnemyTotem2ViewMono enemyTotem2ViewMono = null!;
        [SerializeField] EnemyTotem3ViewMono enemyTotem3ViewMono = null!;
        AbstractEnemyViewMono _selectedEnemyViewMono = null!;

        public override void SetDomain(IEnemyViewParamData enemyParamData)
        {
            Debug.Log("SetDomain enemy enum: " + enemyParamData.GetEnemyEnum());
            SwitchEnemyView(enemyParamData.GetEnemyEnum());
            _selectedEnemyViewMono.SetDomain(enemyParamData);
        }
        
        void SwitchEnemyView(EnemyEnum enemyEnum)
        {
            enemyNormalViewMono.gameObject.SetActive(false);
            enemyBoss1ViewMono.gameObject.SetActive(false);
            enemyBoss2ViewMono.gameObject.SetActive(false);
            enemyBoss3ViewMono.gameObject.SetActive(false);
            enemySpecialViewMono.gameObject.SetActive(false);
            enemyTotem2ViewMono.gameObject.SetActive(false);
            enemyTotem3ViewMono.gameObject.SetActive(false);

            switch (enemyEnum)
            {
                case EnemyEnum.YellowBoss:
                    enemyBoss1ViewMono.gameObject.SetActive(true);
                    _selectedEnemyViewMono = enemyBoss1ViewMono;
                    break;
                case EnemyEnum.RedBoss:
                    enemyBoss2ViewMono.gameObject.SetActive(true);
                    _selectedEnemyViewMono = enemyBoss2ViewMono;
                    break;
                case EnemyEnum.BlueBoss:
                    enemyBoss3ViewMono.gameObject.SetActive(true);
                    _selectedEnemyViewMono = enemyBoss3ViewMono;
                    break;
                case EnemyEnum.SpecialRed:
                case EnemyEnum.SpecialBlue:
                case EnemyEnum.SpecialYellow:
                    enemySpecialViewMono.gameObject.SetActive(true);
                    _selectedEnemyViewMono = enemySpecialViewMono;
                    break;
                case EnemyEnum.Totem2:
                    enemyTotem2ViewMono.gameObject.SetActive(true);
                    _selectedEnemyViewMono = enemyTotem2ViewMono;
                    break;
                case EnemyEnum.Totem3:
                    enemyTotem3ViewMono.gameObject.SetActive(true);
                    _selectedEnemyViewMono = enemyTotem3ViewMono;
                    break;
                default:
                    enemyNormalViewMono.gameObject.SetActive(true);
                    _selectedEnemyViewMono = enemyNormalViewMono;
                    break;
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