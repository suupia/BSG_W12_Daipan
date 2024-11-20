#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Daipan.LevelDesign.Net;
using Daipan.Transporter;
using Daipan.Transporter.Scripts;
using Fusion;
using UnityEngine;
using VContainer;

namespace Daipan.AntiNet.Scripts
{
    public class CostUpdater : IUpdate
    {
        readonly ICostValueParam _costValueParam;
        readonly SpawnEnemyCostValue _spawnEnemyCost;
        readonly bool _isAnti;
        double _timer;

        [Inject]
        public CostUpdater(
            ICostValueParam costValueParam
            , SpawnEnemyCostValue spawnEnemyCost
            , NetworkRunner runner
            , PlayerDataTransporterNetWrapper transporterNetWrapper)
        {
            _isAnti = transporterNetWrapper.GetPlayerRoleEnum(runner.LocalPlayer) == PlayerRoleEnum.Anti; ;
            _costValueParam = costValueParam;
            _spawnEnemyCost = spawnEnemyCost;
        }

        void IUpdate.Update()
        {
            if (!_isAnti) return;
            _timer += Time.deltaTime;

            // コスト回復
            if (_timer >= _costValueParam.IncreaseCostTime)
            {
                _spawnEnemyCost.IncreaseValue(_costValueParam.IncreasedCostValue);
                _timer = 0;
            }
        }
    }
}