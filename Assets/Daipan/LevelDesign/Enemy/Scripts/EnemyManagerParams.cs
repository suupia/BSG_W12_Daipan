#nullable enable 
using System;
using Daipan.Comment.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy/ManagerParameters", order = 1)]
    public sealed class EnemyManagerParams : ScriptableObject
    {
        [Header("エネミー全体のレベルデザインはこちら！！")]
        [Space(30)]




        [Header("エネミー生成のクールタイム")]
        [Min(0)]
        public float spawnDelaySec;

        [Header("ボスの生成周期 (n回通常敵を倒したら生成)")]
        [Min(0)]
        public int spawnBossAmount;


        [Header("使用するエネミーを設定してください。")]
        public List<EnemyLifeParams> enemyLifeParams;
    }

    [Serializable]
    public class EnemyLifeParams
    {
        [Header("使用するエネミーを設定")]
        public EnemyParams enemyParams;

        [Header("エネミーの生成割合 (相対的に指定可)")]
        [Min(0)]
        public float spawnRatio;

        [Header("台パンの影響を受けるイライラ度の閾値")]
        [Min((0))]
        public int daipanThreshold;

        [Header("台パンされたときに死ぬ確率 (0～1)")]
        [Range(0.0f, 1.0f)]
        [Tooltip("最終的には別途、イライラ度に応じた確率シートを作成する")]
        public float daipanProbability;

    }
}