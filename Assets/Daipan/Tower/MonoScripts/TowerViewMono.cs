#nullable enable
using System;
using UnityEngine;

namespace Daipan.Tower.MonoScripts
{
    public sealed class TowerViewMono : MonoBehaviour
    {
        [SerializeField] Animator animator = null!;
        [SerializeField] SpriteRenderer towerFullRender = null!;
        [SerializeField] GameObject lightView = null!;
        Material? _towerFullMaterial;

        void Awake()
        {
            _towerFullMaterial = towerFullRender.material;
        }
        
        public void SetRatio(double ratio)
        {
            if (_towerFullMaterial == null)
            {
                Debug.LogWarning("_towerFullMaterial is null");
                return;
            }

            _towerFullMaterial.SetFloat("_Ratio", (float)ratio);
        }
        
        public void SwitchLight(bool isOn)
        {
            lightView.SetActive(isOn);
        }

        public void Daipan()
        {
            animator.SetTrigger("OnDaipan");
        }
    }
}