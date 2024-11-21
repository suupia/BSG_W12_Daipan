#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Daipan.LevelDesign.Net
{
    public interface IEnemySpawnedCostParam
    {
        public int NormalEnemyCost { get; }
        public int BossEnemyCost { get; }
    }
}