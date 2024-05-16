#nullable enable
using Daipan.Battle.interfaces;
using UnityEngine;
using VContainer;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerAttack
    {
        readonly PlayerAttackParameter _parameter;

        [Inject]
        public PlayerAttack(PlayerParameter parameter)
        {
            _parameter = parameter.attack;
        }

        public void Attack(int attackIndex)
        {
            Debug.Log($"Temp PlayerAttack Attack({attackIndex}) , Temp AttackPower : {_parameter.AttackAmount}");
        }

        public void WAttack(IHpSetter hpSetter)
        {
            hpSetter.CurrentHp -= _parameter.WAttackAmount;
        }

        public void AAttack(IHpSetter hpSetter)
        {
            hpSetter.CurrentHp -= _parameter.AAttackAmount;
        }

        public void SAttack(IHpSetter hpSetter)
        {
            hpSetter.CurrentHp -= _parameter.SAttackAmount;
        }
    }
}