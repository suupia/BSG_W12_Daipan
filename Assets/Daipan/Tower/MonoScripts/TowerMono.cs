#nullable enable
using System.Collections.Generic;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.MonoScripts;
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
        PlayerMono? _playerMono;

        [Inject]
        public void Initialize(
            TowerParamsConfig towerParamsConfig
            )
        {
            _towerParamsConfig = towerParamsConfig;
        }

        void Update()
        {
            if (_playerMono == null)
            {
                _playerMono = FindObjectOfType<PlayerMono>();
                if (_playerMono == null) return;
            }
            
            // Update tower gauge
            var playerHpRatio = _playerMono.CurrentHp / (float)_playerMono.MaxHp;
            towerViewMono?.SetRatio(playerHpRatio);
            towerViewMono?.SwitchLight(playerHpRatio < _towerParamsConfig.GetLightIsOnRatio());
             
        }


    }
 
}

