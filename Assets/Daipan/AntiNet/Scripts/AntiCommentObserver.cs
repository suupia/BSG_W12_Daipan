#nullable enable
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Daipan.AntiNet.Scripts
{
    public class AntiCommentObserver
    {
        public Action? OnOverCount;
        private int _antiCommentCount;
        private IAntiCommentParam _antiCommentParam;

        [Inject]
        public AntiCommentObserver(
            AntiStateValue antiStateValue
            , IAntiCommentParam antiCommentParam)
        {
            OnOverCount += () =>
            {
                Debug.Log($"Comment Count is over {antiCommentParam.BanCount} , so BAN!!");
                antiStateValue.SetBan();
                ResetCount();
            };

            _antiCommentParam = antiCommentParam;
        }
        public void UpCount()
        {
            _antiCommentCount++;
            if (_antiCommentCount >= _antiCommentParam.BanCount) OnOverCount?.Invoke();
            Debug.Log($"Current Comment Count is {_antiCommentCount}");
        }
        public void ResetCount()
        {
            _antiCommentCount = 0;
            Debug.Log($"Current Comment Count is reset");
        }
    }
}