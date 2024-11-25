#nullable enable

using System;
using Daipan.LevelDesign.Net;
using R3;
using UnityEngine;
using VContainer;

namespace Daipan.AntiNet.Scripts
{
    public class AntiStateValue
    {
        public AntiStateEnum AntiStateEnum { get => _antiStateEnum; }
        public bool IsSpecialFever { get; private set; }

        private AntiStateEnum _antiStateEnum;
        private IDisposable? _returnNormalTimer;
        readonly IAntiStateParam _antiStateParam;

        [Inject]
        public AntiStateValue(
            IAntiStateParam antiStateParam
        )
        {
            SetNormal();
            _antiStateParam = antiStateParam;
        }


        public void SetNormal()
        {
            _antiStateEnum = AntiStateEnum.Normal;
            IsSpecialFever = false;

            _returnNormalTimer?.Dispose();
        }
        public void SetBan()
        {
            _antiStateEnum = AntiStateEnum.BAN;
            IsSpecialFever = false;

            _returnNormalTimer?.Dispose();
            _returnNormalTimer = Observable.Timer(TimeSpan.FromSeconds(_antiStateParam.BanTime))
                .Subscribe(_ => { SetNormal(); });
        }
        public void SetFever(bool isSpecial)
        {
            _antiStateEnum = AntiStateEnum.FEVER;
            IsSpecialFever = isSpecial;

            _returnNormalTimer?.Dispose();

            _returnNormalTimer = Observable.Timer(TimeSpan.FromSeconds(_antiStateParam.FeverTime))
                .Subscribe(_ => { SetNormal(); });
        }

    }
}