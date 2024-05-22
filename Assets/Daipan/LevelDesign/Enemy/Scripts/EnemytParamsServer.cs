#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;


namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyParamsServer
    {
        readonly EnemyParams _enemyParams = null;
        readonly EnemyPosition _enemyPosition = null;

        [Inject]
        EnemyParamsServer (EnemyParams enemyParams, EnemyPosition enemyPosition)
        {
            _enemyParams = enemyParams;
            _enemyPosition = enemyPosition;
        }

        public Vector3 GetSpawnedPositionRandom()
        {
            List<Vector3> position = new();
            List<float> ratio = new();
            float totalRatio = 0f;

            foreach (var point in _enemyPosition.enemySpawnedPoints)
            {
                position.Add(point.transform.position);
                ratio.Add(point.ratio);
                totalRatio += point.ratio;
            }

            float random = Random.value * totalRatio;

            int i;
            for (i = 0;i < ratio.Count; i++)
            {
                if (random < ratio[i])
                {
                    break;
                }
                else
                {
                    random -= ratio[i];
                }
            }

            return position[i];

        }
        public Vector3 GetDespawnedPosition()
        {
            return _enemyPosition.enemyDespawnedPoint.position;
        }
    }
}