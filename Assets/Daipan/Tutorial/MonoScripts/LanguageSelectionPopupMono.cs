#nullable enable
using System;
using UnityEngine;

namespace Daipan.Tutorial.MonoScripts
{
    public sealed class LanguageSelectionPopupMono : MonoBehaviour
    {
        [SerializeField] GameObject popupView = null!;

        void Awake()
        {
            HidePopup(); 
        }

        public void ShowPopup()
        {
            popupView.SetActive(true);
        }
        
        public void HidePopup()
        {
            popupView.SetActive(false);
        }
    } 
}

