#nullable enable
using System;
using Cysharp.Threading.Tasks;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Daipan.Sound.MonoScripts;
using Fusion;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.Enemy.MonoScripts
{
    public sealed class EnemyViewNet : AbstractEnemyViewCompositeNet
    {
        public IEnemyViewMono GetSelectedEnemyViewMono => _selectedEnemyViewMono;
        [SerializeField] EnemyNormalViewNet enemyNormalViewMono = null!;
        [SerializeField] EnemyBoss1ViewNet enemyBoss1ViewMono = null!; // Tank
        [SerializeField] EnemyBoss2ViewNet enemyBoss2ViewMono = null!; // 筋肉
        [SerializeField] EnemyBoss3ViewNet enemyBoss3ViewMono = null!; // 素早い
        [SerializeField] EnemySpecialViewNet enemySpecialViewMono = null!;
        [SerializeField] EnemyTotem2ViewNet enemyTotem2ViewMono = null!;
        [SerializeField] EnemyTotem3ViewNet enemyTotem3ViewMono = null!;
        AbstractEnemyViewNet _selectedEnemyViewMono = null!;
        Action _onDied = () => { };
        Action _onDaipaned = () => { };

        public override void SetDomain(IEnemyViewParamData enemyParamData)
        {
            Debug.Log("SetDomain enemy enum: " + enemyParamData.GetEnemyEnum());
            SwitchEnemyView(enemyParamData.GetEnemyEnum());
            _selectedEnemyViewMono.SetDomain(enemyParamData);
        }
        
        public override void SetOnDiedCallback(Action onDied)
        {
            _onDied = onDied;
        }
        
        public override void SetOnDaipanedCallback(Action onDaipaned)
        {
            Debug.Log($"[EnemyViewNet] SetOnDaipanedCallback() , Object.StateAuthority: {Object.HasStateAuthority}");
            _onDaipaned = onDaipaned;
        }
        public override void SetSpecialBlackCallback(Action onSpecialBlack)
        {
            if(_selectedEnemyViewMono is IEnemySpecialView specialView)
                specialView.SetSpecialBlackCallback(onSpecialBlack); 
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

        public override void Died()
        {
            Debug.Log($"[EnemyViewNet] Died() , Object.StateAuthority: {Object.HasStateAuthority}");
            _selectedEnemyViewMono.Died(_onDied);
            RpcDiedSe();
        }
        [Rpc(RpcSources.All, RpcTargets.All)]
        void RpcDiedSe()
        {
            EnemyDieSe.PlayEnemyDieSe().Forget();
        }

        public override void Daipaned()
        {
            Debug.Log($"[EnemyViewNet] Daipaned() , Object.StateAuthority: {Object.HasStateAuthority}");
            _selectedEnemyViewMono.Daipaned(_onDaipaned);
        }

        public override void Highlight(bool isHighlighted)
        {
            _selectedEnemyViewMono.Highlight(isHighlighted);
        }
    }
    
    public static class EnemyDieSe
    {
        public static async UniTaskVoid PlayEnemyDieSe()
        {
            SoundManager.Instance?.PlaySe(SeEnum.EnemyDieContraction);
            await UniTask.Delay(260);
            SoundManager.Instance?.PlaySe(SeEnum.EnemyDieExplosion);
        }
    }
}