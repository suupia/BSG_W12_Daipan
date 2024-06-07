using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Daipan.LevelDesign.Tower.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Tower/Parameters", order = 1)]
    public sealed class TowerParams : ScriptableObject
    {
        [Header("タワーのレベルデザインはこちら！！")]
        [Space(30)]
        
        public double hpAmount;
        public Sprite backgroundSprite;
        public Sprite emptySprite;
        public Sprite fullSprite;
        public Sprite lightSprite;
    }

}