#nullable enable
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;
using Daipan.Stream.Interfaces;
using Daipan.StreamerNet.MonoScripts;
using Daipan.Transporter;
using Daipna.StreamerNet.Scripts;
using Fusion;
using UnityEngine;
using VContainer;

namespace Daipan.Stream.Scripts
{
    public sealed class ViewerNumberNetwork : IViewerNumber
    {
        public int Number { get; private set; }
        public int Difference { get; private set; }

        readonly NetworkRunner _runner;
        readonly RpcReceiverNetWrapper _rpcReceiverNetWrapper;
        [Inject]
        public ViewerNumberNetwork(
            NetworkRunner runner
            , RpcReceiverNetWrapper rpcReceiverNetWrapper
            , PlayerDataTransporterNetWrapper playerDataTransporterNetWrapper)
        {
            _runner = runner;
            _rpcReceiverNetWrapper = rpcReceiverNetWrapper;
            Number = 10000;
        }

        public void IncreaseViewer(int amount)
        {
            // [Prerequisite]
            if (amount < 0) Debug.LogWarning($"ViewerNumber.IncreaseViewer() amount is negative : {amount}");

            Number += amount;

            _rpcReceiverNetWrapper.RpcReceiverNet.SetViewerRPC(_runner.LocalPlayer, Number);

            Difference = amount;
        }

        public void DecreaseViewer(int amount)
        {
            // [Prerequisite]
            if (amount < 0) Debug.LogWarning($"ViewerNumber.DecreaseViewer() amount is negative : {amount}");

            int preNumber = Number;

            Number = Mathf.Max(0, Number - amount);

            _rpcReceiverNetWrapper.RpcReceiverNet.SetViewerRPC(_runner.LocalPlayer, Number);

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