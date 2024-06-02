using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    [CreateAssetMenu(fileName = "EnemyParamsManager", menuName = "ScriptableObjects/Enemy/EnemyParamsManager", order = 1)]
    public sealed class EnemyParamsManager : ScriptableObject
    {
        [Header("エネミー全体のレベルデザインはこちら")]
        [Space(30)]

        public EnemyLevelDesignParam enemyLevelDesignParam = null!;
        
        [Header("使用するエネミーを設定してください。")]
        public List<EnemyParam> enemyParams = null!;

        [Header("時間による変化を設定してください。")]
        [Header("設定されてない場合は上のデフォルト値が使用されます。")]
        public List<EnemyTimeLineParam> enemyTimeLines = null!;

    }



}

