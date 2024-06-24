#nullable enable
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;

namespace Daipan.Comment.Scripts
{
    public static class ViewerCalculatorModule
    {
        public static void IncreaseViewer(ViewerNumber viewerNumber, int baseIncrementAmount,ComboCounter comboCounter, IComboMultiplier comboMultiplier)
        {
            var multipliedAmount = (int)(baseIncrementAmount * comboMultiplier.CalculateComboMultiplier(comboCounter.ComboCount));
            if (multipliedAmount > 0) viewerNumber.IncreaseViewer(multipliedAmount);
            else viewerNumber.IncreaseViewer(multipliedAmount);
        }

        public static void DecreaseViewer(ViewerNumber viewerNumber, int baseDecrementAmount, ComboCounter comboCounter, IComboMultiplier comboMultiplier)
        {
            var multipliedAmount = (int)(baseDecrementAmount * comboMultiplier.CalculateComboMultiplier(comboCounter.ComboCount));
            if (multipliedAmount > 0) viewerNumber.DecreaseViewer(multipliedAmount);
            else viewerNumber.DecreaseViewer(multipliedAmount);
        }
    }
}