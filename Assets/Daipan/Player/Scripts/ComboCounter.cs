#nullable enable
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Stream.Scripts;

namespace Daipan.Player.Scripts
{
    public class ComboCounter
    {
        public int ComboCount { get; set; }
        readonly ViewerNumber _viewerNumber;
        
        public ComboCounter(
            IComboParamContainer comboParamContainer,
            ViewerNumber viewerNumber)
        {
            _viewerNumber = viewerNumber; 
        }
        
        public void IncreaseCombo()
        {
            ComboCount++;
        }
        
        public void ResetCombo()
        {
            ComboCount = 0;
        }
    }
}

