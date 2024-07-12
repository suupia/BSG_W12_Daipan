#nullable enable
using System.Collections.Generic;
using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.Enemy.LevelDesign.Scripts
{
    [CreateAssetMenu(fileName = "FinalBossParamManager", menuName = "ScriptableObjects/Enemy/FinalBossParamManager", order = 1)]
    public sealed class FinalBossParamManager : ScriptableObject
    {
        [Header("Final Waveに関するレベルデザインはこちら。")] 
        [Space] 
        [Header("FinalBOSSに関するパラメータを設定してください。")]
        public FinalBossParam finalBossParam = null!;
        
        [Header("FinalWaveに関するレベルデザインはこちら")]
        public FinalWaveParam finalWaveParam = null!;
    } 
}

