#nullable enable
using Daipan.Battle.interfaces;
using Daipan.LevelDesign.Player.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerAttack
    {
        readonly PlayerParamConfig _playerParamConfig;

        [Inject]
        public PlayerAttack(PlayerParamConfig playerParamConfig)
        {
            _playerParamConfig = playerParamConfig;
        }

        public void Attack(int attackIndex)
        {
            Debug.Log($"Temp PlayerAttack Attack({attackIndex}) , Temp AttackPower : {_playerParamConfig.GetAttackAmount().AttackAmount}");
        }

        public void WAttack(IHpSetter hpSetter)
        {
            hpSetter.CurrentHp -= _playerParamConfig.GetAttackAmount().WAttackAmount;
        }

        public void AAttack(IHpSetter hpSetter)
        {
            hpSetter.CurrentHp -= _playerParamConfig.GetAttackAmount().AAttackAmount;
        }

        public void SAttack(IHpSetter hpSetter)
        {
            hpSetter.CurrentHp -= _playerParamConfig.GetAttackAmount().SAttackAmount;
        }
    }
}