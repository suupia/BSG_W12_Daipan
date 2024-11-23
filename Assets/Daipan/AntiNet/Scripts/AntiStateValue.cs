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
        //

        public AntiStateEnum AntiStateEnum { get => _antiStateEnum; }

        private AntiStateEnum _antiStateEnum;
        private IDisposable? _banToNormalTimer;

        public AntiStateValue()
        {
            SetNormal();
        }


        public void SetNormal()
        {
            _antiStateEnum = AntiStateEnum.Normal;

            _banToNormalTimer?.Dispose();
        }
        public void SetBan()
        {
            _antiStateEnum = AntiStateEnum.BAN;

            _banToNormalTimer?.Dispose();
            _banToNormalTimer = Observable.Timer(TimeSpan.FromSeconds(banTime))
                .Subscribe(_ => { SetNormal(); });
        }
        public void SetFever()
        {
            _antiStateEnum = AntiStateEnum.FEVER;

            _banToNormalTimer?.Dispose();
        }

    }
}