#nullable enable
using System.Collections.Generic;
using UnityEngine;

namespace Daipan.Player.LevelDesign.Scripts
{
    [CreateAssetMenu(fileName = "ComboParamManager", menuName = "ScriptableObjects/Player/ComboParamManager", order = 1)]
    public class ComboParamManager : ScriptableObject
    {
       public List<ComboMultiplier> comboParams = null!; 
    }
}