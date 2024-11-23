#nullable enable
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Daipan.AntiNet.Scripts
{
    public class AntiCommentObserver
    {
        // todo 値出す
        readonly int banCount = 3;
        //

        public Action? OnOverCount;
        private int _antiCommentCount;

        [Inject]
        public AntiCommentObserver(AntiStateValue antiStateValue)
        {
            OnOverCount += () =>
            {
                Debug.Log($"Comment Count is over {banCount} , so BAN!!");
                antiStateValue.SetBan();
                ResetCount();
            };
        }
        public void UpCount()
        {
            _antiCommentCount++;
            if (_antiCommentCount >= banCount) OnOverCount?.Invoke();
            Debug.Log($"Current Comment Count is {_antiCommentCount}");
        }
        public void ResetCount()
        {
            _antiCommentCount = 0;
            Debug.Log($"Current Comment Count is reset");
        }
    }
}