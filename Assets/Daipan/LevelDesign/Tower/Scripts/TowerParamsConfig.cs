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
            return _towerPositionMono.towerSpawnTransform.position;
        }

        public Sprite GetCurrentSprite(float HP)
        {
            var i = _towerParams.towerSprites.Where(t => t.hpThreshold >= HP)
                .OrderBy(t => t.hpThreshold).First();

            return i.sprite;
        }
    }

}
