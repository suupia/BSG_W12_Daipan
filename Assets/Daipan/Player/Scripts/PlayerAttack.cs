#nullable enable
using Daipan.Battle.interfaces;
using Daipan.LevelDesign.Player.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerAttack
    {
        readonly PlayerParamDTO _playerParamDto;

        [Inject]
        public PlayerAttack(PlayerParamDTO playerParamDto)
        {
            _playerParamDto = playerParamDto;
        }

        public void WAttack(IHpSetter hpSetter)
        {
            hpSetter.CurrentHp -= _playerParamDto.GetWAttack();
        }

        public void AAttack(IHpSetter hpSetter)
        {
            hpSetter.CurrentHp -= _playerParamDto.GetAAttack();
        }

        public void SAttack(IHpSetter hpSetter)
        {
            hpSetter.CurrentHp -= _playerParamDto.GetSAttack();
        }
    }
}