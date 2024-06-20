#nullable enable
namespace Daipan.Player.Scripts
{
    public class ComboCounter
    {
        public int ComboCount { get; set; }
        
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

