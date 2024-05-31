#nullable enable
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.LevelDesign.Player.Scripts
{

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Player/PlayerParam", order = 1)]
    public sealed class PlayerParam : ScriptableObject
    {
        [Header("プレイヤーのレベルデザインはこちら！！")]
        [Space(30)]

        [Header("プレイヤーのHP")]
        [Min(0)]
        public int hpAmount;

        [Header(("プレイヤーの攻撃力"))]
        public PlayerAttackParam playerAttackParam = null!;
    }


    [Serializable]
    public sealed class PlayerAttackParam
    {

        [Min(0)] public int AttackAmount;
        [Min(0)] public int SAttackAmount = 10;
        [Min(0)] public int WAttackAmount = 20;
        [Min(0)] public int AAttackAmount = 30;
    }
}