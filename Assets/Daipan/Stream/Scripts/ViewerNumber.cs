#nullable enable
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;
using UnityEngine;

namespace Daipan.Stream.Scripts
{
    public sealed class ViewerNumber
    {
        public int Number { get; private set; } 

        public void IncreaseViewer(int amount)
        {
            // [Prerequisite]
            if (amount < 0) Debug.LogWarning($"ViewerNumber.IncreaseViewer() amount is negative : {amount}");
            
            Number += amount; 
        }

        public void DecreaseViewer(int amount)
        {
            // [Prerequisite]
            if (amount < 0) Debug.LogWarning($"ViewerNumber.DecreaseViewer() amount is negative : {amount}");

            Number = Mathf.Max(0, Number - amount); 
        }
    }
}