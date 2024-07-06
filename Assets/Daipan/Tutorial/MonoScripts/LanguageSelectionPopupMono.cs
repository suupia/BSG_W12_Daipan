#nullable enable
using UnityEngine;

namespace Daipan.Tutorial.MonoScripts
{
    internal sealed class LanguageSelectionPopupMono : MonoBehaviour
    {
        [SerializeField] GameObject popupView = null!;
        
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

