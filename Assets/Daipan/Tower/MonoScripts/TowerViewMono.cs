#nullable enable
using System;
using UnityEngine;

namespace Daipan.Tower.MonoScripts
{
    public class TowerViewMono : MonoBehaviour
    {
        [SerializeField] SpriteRenderer towerFullRender = null!;
        [SerializeField] GameObject lightView = null!;
        Material? _towerFullMaterial;

        void Awake()
        {
            _towerFullMaterial = towerFullRender.material;
        }
        
        public void SetRatio(float ratio)
        {
            if (_towerFullMaterial == null)
            {
                Debug.LogWarning("_towerFullMaterial is null");
                return;
            }

            _towerFullMaterial.SetFloat("_Ratio", ratio);
        }
        
        public void SwitchLight(bool isOn)
        {
            lightView.SetActive(isOn);
        }
    }
}