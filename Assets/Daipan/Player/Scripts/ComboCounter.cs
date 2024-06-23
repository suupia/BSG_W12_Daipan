#nullable enable
using System.Collections.Generic;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Stream.Scripts;

namespace Daipan.Player.Scripts
{
    public sealed class ComboCounter
    {
        public int ComboCount { get; private set; }
        readonly ViewerNumber _viewerNumber;
        readonly IEnumerator<ComboParam> _comboParamsEnumerator;
        ComboParam _currentComboParam;


        public ComboCounter(
            IComboParamContainer comboParamContainer,
            ViewerNumber viewerNumber)
        {
            _viewerNumber = viewerNumber;
            _comboParamsEnumerator = comboParamContainer.GetComboParams().GetEnumerator();
            _currentComboParam = GetNextComboParam(_comboParamsEnumerator);
        }

        public void IncreaseCombo()
        {
            ComboCount++;

            if (ComboCount >= _currentComboParam.comboThreshold)
            {
                _viewerNumber.IncreaseViewer((int)(_viewerNumber.Number * (_currentComboParam.comboMultiplier - 1)));
                _currentComboParam = GetNextComboParam(_comboParamsEnumerator);
            }
        }

        public void ResetCombo()
        {
            ComboCount = 0;
            _comboParamsEnumerator.Reset();
            _currentComboParam = GetNextComboParam(_comboParamsEnumerator);
        }

        static ComboParam GetNextComboParam(IEnumerator<ComboParam> comboParamsEnumerator)
        {
            if (comboParamsEnumerator.MoveNext() && comboParamsEnumerator.Current != null)
                return comboParamsEnumerator.Current;
            // default value 
            return new ComboParam { comboThreshold = int.MaxValue, comboMultiplier = 1 };
        }
    }
}