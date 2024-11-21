#nullable enable
using System;
using System.Collections.Generic;

namespace Daipan.AntiNet.Scripts
{
    public class AntiCommentObserver
    {
        // todo 値出す
        readonly int banCount = 3;
        //

        public Action? OnOverCount;
        private int _antiCommentCount;


        public void CountUp()
        {
            _antiCommentCount++;
            if (_antiCommentCount >= banCount) OnOverCount?.Invoke();

        }
        public void ResetCount()
        {
            _antiCommentCount = 0;
        }
    }
}