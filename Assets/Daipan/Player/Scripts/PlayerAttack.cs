#nullable enable
using Daipan.Battle.interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerAttack
    {
        readonly IPlayerParamData _playerParamData;

        public PlayerAttack(IPlayerParamData playerParamData)
        {
            _playerParamData = playerParamData;
        }

        //public void Attack(IHpSetter hpSetter)
        //{
        //    Debug.Log("Attack Enemy");
        //    hpSetter.CurrentHp -= _playerParamData.GetAttack();
        //    Debug.Log($"_playerParamData.GetAttack() = {_playerParamData.GetAttack()}");
        //}
    }
}