#nullable enable

using System;
using R3;
using UnityEngine;

namespace Daipan.AntiNet.Scripts
{
    public class AntiStateValue
    {
        // todo 値出し
        float banTime = 5f;
        float feverTime = 10f;
        //

        public AntiStateEnum AntiStateEnum { get => _antiStateEnum; }
        public bool IsSpecialFever { get; private set; }

        private AntiStateEnum _antiStateEnum;
        private IDisposable? _returnNormalTimer;

        public AntiStateValue()
        {
            SetNormal();
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
            _returnNormalTimer = Observable.Timer(TimeSpan.FromSeconds(banTime))
                .Subscribe(_ => { SetNormal(); });
        }
        public void SetFever(bool isSpecial)
        {
            _antiStateEnum = AntiStateEnum.FEVER;
            IsSpecialFever = isSpecial;

            _returnNormalTimer?.Dispose();

            _returnNormalTimer = Observable.Timer(TimeSpan.FromSeconds(feverTime))
                .Subscribe(_ => { SetNormal(); });
        }

    }
}