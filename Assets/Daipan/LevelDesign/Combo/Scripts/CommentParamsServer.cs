#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.Comment.Scripts;
using System.Linq;
using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.U2D;
using VContainer;


namespace Daipan.LevelDesign.Combo.Scripts
{
    public class ComboParamsManager
    {
        readonly ComboPositionMono _comboPositionMono;

        [Inject]
        ComboParamsManager(ComboPositionMono commentPosition)
        {
            _comboPositionMono = commentPosition;
        }


        public Transform GetComboParent()
        {
            return _comboPositionMono.ComboParent;
        }
    }
}