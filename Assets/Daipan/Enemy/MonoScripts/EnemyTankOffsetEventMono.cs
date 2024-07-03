#nullable enable
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.Enemy.MonoScripts
{
    public class EnemyTankOffsetEventMono : MonoBehaviour
    {
        [SerializeField] double[] offsetRatioMove = { 0, 0, 0, 0, 0, 0, 0, 0 };
        [SerializeField] double[] offsetRatioAttack = { 0, 0, 0, 0, 0, 0, 0, 0 };

        [Header("Attackの時の傾ける角度（弧度法）")] [SerializeField]
        float attackAngle = 0;

        Material _tankGaugeMaterial = null!;
        [SerializeField] Animator animator = null!;

        // Tankの画像が全体の画像のサイズに合わせられているため、FillMin, FillMaxで調整
        // FinMinで0を、FillMaxで1を取るようにする
        const double FillMin = 0.1;
        const double FillMax = 0.6;

        public double Ratio { get; set; }

        public double CurrentOffsetRatioMove { get; private set; }
        public double CurrentOffsetRatioAttack { get; private set; }

        void Awake()
        {
            _tankGaugeMaterial = animator.GetComponent<SpriteRenderer>().material;
        }

        void Frame0()
        {
            CurrentOffsetRatioMove = offsetRatioMove[0];
            CurrentOffsetRatioAttack = offsetRatioAttack[0];
            Update();
        }

        void Frame1()
        {
            CurrentOffsetRatioMove = offsetRatioMove[1];
            CurrentOffsetRatioAttack = offsetRatioAttack[1];
            Update();
        }

        void Frame2()
        {
            CurrentOffsetRatioMove = offsetRatioMove[2];
            CurrentOffsetRatioAttack = offsetRatioAttack[2];
            Update();
        }

        void Frame3()
        {
            CurrentOffsetRatioMove = offsetRatioMove[3];
            CurrentOffsetRatioAttack = offsetRatioAttack[3];
            Update();
        }

        void Frame4()
        {
            CurrentOffsetRatioMove = offsetRatioMove[4];
            CurrentOffsetRatioAttack = offsetRatioAttack[4];
            Update();
        }

        void Frame5()
        {
            CurrentOffsetRatioMove = offsetRatioMove[5];
            CurrentOffsetRatioAttack = offsetRatioAttack[5];
            Update();
        }

        void Frame6()
        {
            CurrentOffsetRatioMove = offsetRatioMove[6];
            CurrentOffsetRatioAttack = offsetRatioAttack[6];
            Update();
        }

        void Frame7()
        {
            CurrentOffsetRatioMove = offsetRatioMove[7];
            CurrentOffsetRatioAttack = offsetRatioAttack[7];
            Update();
        }

        void Update()
        {
            var clipName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            // あまりよくないが、clipNameで分岐
            if (clipName == "Boss1_AttackMotion_Tank")
            {
                _tankGaugeMaterial.SetFloat("_Ratio",
                    (float)((Ratio + CurrentOffsetRatioAttack) * (FillMax - FillMin) + FillMin));
                _tankGaugeMaterial.SetFloat("_RotationAngle", attackAngle);
            }
            else
            {
                _tankGaugeMaterial.SetFloat("_Ratio",
                    (float)((Ratio + CurrentOffsetRatioMove) * (FillMax - FillMin) + FillMin));
                _tankGaugeMaterial.SetFloat("_RotationAngle", 0);
            }
        }
    }
}