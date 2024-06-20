#nullable enable
using System.Collections.Generic;
using Daipan.Player.LevelDesign.Scripts;

namespace Daipan.Player.LevelDesign.Interfaces
{
    public interface IComboParamContainer
    {
       IEnumerable<ComboParam> GetComboParams(); 
    }
}