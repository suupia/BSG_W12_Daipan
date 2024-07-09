#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Battle.scripts;
using Daipan.Stream.Scripts;
using R3;

namespace Daipan.Battle.Scripts
{
    public class ResultState : IDisposable
    {
        readonly StreamTimer _streamTimer;
        readonly List<IDisposable> _disposables = new();
        
        public bool IsInResult { get; private set; }

        public ResultState(
            StreamTimer streamTimer
            , ResultViewMono resultViewMono
        )
        {
            _streamTimer = streamTimer;
            
            _disposables.Add(Observable.EveryUpdate().Subscribe(_ =>
            {
                if (_streamTimer.CurrentProgressRatio >= 1)
                {
                    resultViewMono.ShowResult();
                    IsInResult = true;
                }
            }));
            
        }
        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
        ~ResultState()
        {
            Dispose();
        }
            
    } 
}

