#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Core;
using Daipan.Daipan;
using Daipan.Stream.Interfaces;
using Daipan.Transporter;
using Daipna.StreamerNet.Scripts;
using Fusion;
using R3;
using UnityEngine;
using VContainer;

namespace Daipan.Stream.Scripts
{
    public sealed class IrritatedGaugeValueNetwork : IIrritatedGaugeValue
    {
        readonly bool _isStreamer;
        readonly IrritatedParams _irritatedParams;
        readonly RpcReceiverNetWrapper _rpcReceiverNetWrapper;

        [Inject]
        public IrritatedGaugeValueNetwork(
            double maxValue
            , IrritatedParams irritatedParams
            , NetworkRunner runner
            , PlayerDataTransporterNetWrapper playerDataTransporterNetWrapper
            , RpcReceiverNetWrapper rpcReceiverNetWrapper)
        {
            MaxValue = maxValue;
            _irritatedParams = irritatedParams;
            Value = 0;

            _isStreamer = playerDataTransporterNetWrapper.GetPlayerRoleEnum(runner.LocalPlayer) == PlayerRoleEnum.Streamer;

            _rpcReceiverNetWrapper = rpcReceiverNetWrapper;
        }

        public double MaxValue { get; }
        public bool IsFull => Value >= MaxValue;

        public double Ratio => Value / MaxValue;

        public double Value
        {
            get => _value;
            private set => _value = value;
        }
        private double _value;

        public IReadOnlyList<double> RatioTable => _irritatedParams.RatioTable;


        public int CurrentIrritatedStage
        {
            get
            {
                for (var i = 0; i < RatioTable.Count; i++)
                {
                    if (Ratio >= RatioTable[i]) continue;
                    return i;
                }

                return RatioTable.Count;
            }
        }


        public void IncreaseValue(double amount)
        {
            // [Precondition]
            if (amount < 0) Debug.LogWarning($"IrritatedValue.IncreaseValue() amount is negative : {amount}");
            // Debug.Log($"IrritatedValue.IncreaseValue() amount : {amount}");
            Value = Math.Min(MaxValue, Value + amount);

            if (_isStreamer)
            {
                _rpcReceiverNetWrapper.RpcReceiverNet.SetIrritatedValueRPC(Value);
            }
        }

        public void DecreaseValue(double amount)
        {
            // [Precondition]
            if (amount < 0) Debug.LogWarning($"IrritatedValue.DecreaseValue() amount is negative : {amount}");

            Value = Math.Max(0, Value - amount);

            if (_isStreamer)
            {
                _rpcReceiverNetWrapper.RpcReceiverNet.SetIrritatedValueRPC(Value);
            }
        }

        public void Reset()
        {
            Value = 0;
            if (_isStreamer)
            {
                _rpcReceiverNetWrapper.RpcReceiverNet.SetIrritatedValueRPC(Value);
            }
        }
        public void SetValue(double amount)
        {
            Value = amount;
        }
    }
}