#nullable enable
using TMPro;
using UnityEngine;

namespace Daipan.Player.MonoScripts
{
    public class ComboViewMono : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI comboText = null!;
        
        public void UpdateComboText(int comboCount)
        {
            comboText.text = $"{comboCount}";
        }
    
    } 
}

