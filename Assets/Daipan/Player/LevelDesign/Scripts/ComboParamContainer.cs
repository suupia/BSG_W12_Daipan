#nullable enable
using System.Collections.Generic;
using Daipan.Player.LevelDesign.Interfaces;

namespace Daipan.Player.LevelDesign.Scripts
{
    public class ComboParamContainer : IComboParamContainer
    {
        readonly ComboParamManager _comboParamManager;
        public ComboParamContainer(ComboParamManager comboParamManager)
        {
            _comboParamManager = comboParamManager;
        }
        
        public IEnumerable<ComboParam> GetComboParams()
        {
            return _comboParamManager.comboParams;
        }
    }
}