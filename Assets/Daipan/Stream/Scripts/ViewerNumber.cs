#nullable enable
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;
using Daipan.Stream.Interfaces;
using UnityEngine;

namespace Daipan.Stream.Scripts
{
    public sealed class ViewerNumber : IViewerNumber
    {
        public int Number { get; private set; }
        public int Difference { get; private set; }

        public void IncreaseViewer(int amount)
        {
            // [Prerequisite]
            if (amount < 0) Debug.LogWarning($"ViewerNumber.IncreaseViewer() amount is negative : {amount}");

            Number += amount;
            Difference = amount;
        }

        public void DecreaseViewer(int amount)
        {
            // [Prerequisite]
            if (amount < 0) Debug.LogWarning($"ViewerNumber.DecreaseViewer() amount is negative : {amount}");

            int preNumber = Number;

            Number = Mathf.Max(0, Number - amount);

            Difference = preNumber - Number;
        }
        public void SetViewer(int amount)
        {
            // [Prerequisite]
            if (amount < 0) Debug.LogWarning($"ViewerNumber.DecreaseViewer() amount is negative : {amount}");
            Difference = amount - Number;
            Number = amount;
        }
    }
}