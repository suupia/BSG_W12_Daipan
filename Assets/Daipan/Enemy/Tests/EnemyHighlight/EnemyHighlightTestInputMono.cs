#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using UnityEngine;

public class EnemyHighlightTestInputMono : MonoBehaviour
{
    [SerializeField] EnemyViewMono enemyViewMono = null!;

    void Start()
    {
       enemyViewMono.SetDomain( new EnemyViewParamRed());
    }

    class EnemyViewParamRed : IEnemyViewParamData
    {
        public EnemyEnum GetEnemyEnum() => EnemyEnum.Red;
        public Color GetBodyColor() => Color.red;
        public Color GetEyeColor() => new(226f / 255f, 248f / 255f, 227f / 255f);
        public Color GetEyeBallColor() => Color.red;
        public Color GetLineColor() =>new(111f / 255f, 87f / 255f, 107f / 255f);
    }
}
