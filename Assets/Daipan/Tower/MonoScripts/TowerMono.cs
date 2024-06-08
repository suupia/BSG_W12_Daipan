#nullable enable
using System.Collections.Generic;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.Scripts;
using Daipan.Tower.MonoScripts;
using JetBrains.Annotations;
//using PlasticPipe.PlasticProtocol.Messages;
using UnityEngine;
using VContainer;

namespace Daipan.Tower.MonoScripts
{
    public class TowerMono : MonoBehaviour
    {
        [SerializeField] TowerViewMono? towerViewMono;
        TowerParamsConfig _towerParamsConfig = null!;
        PlayerHolder _playerHolder = null!;

        [Inject]
        public void Initialize(
            TowerParamsConfig towerParamsConfig,
            PlayerHolder playerHolder)
        {
            _towerParamsConfig = towerParamsConfig;
            _playerHolder = playerHolder;
        }

        void Update()
        {
            // Update tower gauge
            var playerHpRatio = _playerHolder.PlayerMono.CurrentHp / (float)_playerHolder.PlayerMono.MaxHp;
            towerViewMono?.SetRatio(playerHpRatio);
            towerViewMono?.SwitchLight(playerHpRatio < _towerParamsConfig.GetLightIsOnRatio());
             
        }


    }
 
}

