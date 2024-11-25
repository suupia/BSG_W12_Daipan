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
        float feverTime = 5f;
        //

        public AntiStateEnum AntiStateEnum { get => _antiStateEnum; }

        private AntiStateEnum _antiStateEnum;
        private IDisposable? _returnNormalTimer;

        public AntiStateValue()
        {
            SetNormal();
        }


        public void SetNormal()
        {
            _antiStateEnum = AntiStateEnum.Normal;

            _returnNormalTimer?.Dispose();
        }
        public void SetBan()
        {
            _antiStateEnum = AntiStateEnum.BAN;

            _returnNormalTimer?.Dispose();
            _returnNormalTimer = Observable.Timer(TimeSpan.FromSeconds(banTime))
                .Subscribe(_ => { SetNormal(); });
        }
        public void SetFever()
        {
            _antiStateEnum = AntiStateEnum.FEVER;

            _returnNormalTimer?.Dispose();

            _returnNormalTimer = Observable.Timer(TimeSpan.FromSeconds(feverTime))
                .Subscribe(_ => { SetNormal(); });
        }

    }
}