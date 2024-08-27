#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Daipan.LevelDesign.Tower.Scripts;
using UnityEngine;

namespace Daipan.LevelDesign.Enemy.Scripts
{ 
    public sealed class TowerParamsConfig
    {
        readonly TowerPositionMono _towerPositionMono;
        readonly TowerParams _towerParams;

        public TowerParamsConfig(TowerPositionMono towerPositionMono, TowerParams towerParams)
        {
            _towerPositionMono = towerPositionMono;
            _towerParams = towerParams;
        }

        public Vector3 GetTowerSpawnPosition()
        {
            return  _towerPositionMono != null ? _towerPositionMono.towerSpawnTransform.position : Vector3.zero;
        }
        
        public double GetLightIsOnRatio()
        {
            return _towerParams.LightIsOnRatio;
        }

    }

}
