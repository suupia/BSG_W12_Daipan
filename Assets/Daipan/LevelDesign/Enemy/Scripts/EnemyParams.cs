using Daipan.Comment.Scripts;
using Daipan.Enemy.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy/Parameters", order = 1)]
    public sealed class EnemyParams : ScriptableObject
    {
        [Header("エネミーのレベルデザインはこちら！！")]
        [Space(30)]

        [Header("エネミーのタイプ")]
        [Tooltip("None      : 設定しないでください-_-\n" +
                 "W         : Wキーで倒せる敵\n" +
                 "A         : Aキーで倒せる敵\n" +
                 "S         : Sキーで倒せる敵\n" +
                 "Boss      ; ボス")]
        [SerializeField] EnemyType enemyType = EnemyType.None;

        [Header("エネミーのHP")]
        public int HPAmount;

        [Header("エネミーの攻撃力")] 
        public int attackAmount;

        [Header("移動速度 [unit/s]")]
        [Min(0)]
        public float moveSpeed_ups;

        [Header("攻撃間隔")]
        [Min(0)]
        public float attackDelaySec;

        [Header("攻撃範囲")]
        [Min(0)]
        public float attackRange;

        [Header("このエネミーを倒したときの視聴者数の変化")]
        public float diffViewer;


        [Header("スプライト")] 
        public Sprite sprite;

        public EnemyEnum GetEnemyEnum
        {
            get
            {
                EnemyEnumChecker.CheckEnum();
                return EnemyEnum.Values.First(x => x.Name == enemyType.ToString());
            }
        }

        static class EnemyEnumChecker
        {
            static bool _isCheckedEnum;

            public static void CheckEnum()
            {
                if (_isCheckedEnum) return;
                foreach (var type in Enum.GetValues(typeof(EnemyType)).Cast<EnemyType>())
                {
                    var enemy = EnemyEnum.Values.FirstOrDefault(x => x.Name == type.ToString());
                    if (enemy.Equals(default(EnemyEnum))) Debug.LogWarning($"EnemyEnum with name {type.ToString()} not found.");
                }

                _isCheckedEnum = true;
            }
        }

        enum EnemyType
        {
            None,
            W,
            A,
            S,
            Boss
        }
    }
}