#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Daipan.LevelDesign.Net
{
    [CreateAssetMenu(fileName = "NetworkPlayerParamsManager", menuName = "ScriptableObjects/Net/NetworkPlayerParamsManager", order = 1)]
    public sealed class NetworkPlayerParamsManager : ScriptableObject
    {
        [Header("Antiに関するパラメータ")]
        [Header("Enemyの生成コスト")]
        public EnemySpawnedCostParam enemySpawnedCostParam = null!;

        [Header("コスト")]
        public CostValueParam costValueParam = null!;

        [Header("アンチコメント")]
        public AntiCommentParam antiCommentParam = null!;

        [Header("BAN・FEVER")]
        public AntiStateParam antiStateParam = null!;


    }

    [Serializable]
    public sealed class EnemySpawnedCostParam
    {
        [Header("ノーマルエネミーは (x[cost]) で生成される")][Min(0)] public int normalEnemyCost;
        [Header("ボスエネミーは (x[cost]) で生成される")][Min(0)] public int bossEnemyCost;
    }

    [Serializable]
    public sealed class CostValueParam
    {
        [Header("コストの上限 (x[cost])")][Min(0)] public int maxCostValue;
        [Header("コストの初期値 (x[cost])")][Min(0)] public int initializedCostValue;
        [Header("コストの回復は (x[秒]) 毎に行われる")][Min(0)] public int increaseCostTime;
        [Header("コストは一度の回復で (x[cost]) 回復する")][Min(0)] public int increasedCostValue;
    }

    [Serializable]
    public sealed class AntiStateParam
    {
        [Header("(x[秒間]) BAN状態が継続する")][Min(0)] public float banTime;
        [Header("(x[秒間]) FEVER状態が継続する")][Min(0)] public float feverTime;
    }

    [Serializable]
    public sealed class AntiCommentParam
    {
        [Header("(x[文字]) 以下しかアンチコメントが書けない")][Min(0)] public int maxCharacterNumber;
        [Header("(n[回]) アンチコメントを書くとBANされる")][Min(0)] public int banCount;
        [Header("(大きなアンチコメントは (n[回]) の台パンで消える")][Min(0)] public int specialAntiCommentHp;
    }
}