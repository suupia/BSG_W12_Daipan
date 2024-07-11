using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
namespace Daipan.Enemy.LevelDesign.Scripts
{
    [CreateAssetMenu(fileName = "EnemyParamManager", menuName = "ScriptableObjects/Enemy/EnemyParamManager", order = 1)]
    public sealed class EnemyParamsManager : ScriptableObject
    {
        [Header("敵に関するレベルデザインはこちら。")] [Space] [Header("BOSSに関するパラメータを設定してください。")]
        public EnemyLevelDesignParam enemyLevelDesignParam = null!;

        [Header("個々の敵のパラメータを設定してください。")] 
        public List<EnemyParam> enemyParams = null!;

        [FormerlySerializedAs("enemyTimeLineParams")]
        [FormerlySerializedAs("enemyTimeLines")]
        [Header("時間による変化を設定してください。\n" +
                "設定されてない場合はデフォルト値が使用されます。")]
        public List<EnemyWaveParam> enemyWaveParams = null!;
    }
}