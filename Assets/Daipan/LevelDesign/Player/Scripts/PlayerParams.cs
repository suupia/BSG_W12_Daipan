#nullable enable
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.LevelDesign.Player.Scripts
{

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Player/PlayerParameters", order = 1)]
    public sealed class PlayerParams : ScriptableObject
    {
        [Header("プレイヤーのレベルデザインはこちら！！")]
        [Space(30)]

        [Header("プレイヤーのHP")]
        [Min(0)]
        public int HPAmount;

        [Header(("プレイヤーの攻撃力"))]
        [Min(0)]
        public PlayerAttackParameter playerAtatckParameter;
    }


    [Serializable]
    public sealed class PlayerAttackParameter
    {
        public int AttackAmount;
        public int SAttackAmount = 10;
        public int WAttackAmount = 20;
        public int AAttackAmount = 30;
    }
}