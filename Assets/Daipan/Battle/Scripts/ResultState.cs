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
        readonly ResultViewMono _resultViewMono;
        readonly List<IDisposable> _disposables = new();
        
        public bool IsInResult { get; private set; }

        public ResultState(
            StreamTimer streamTimer
            , ResultViewMono resultViewMono
        )
        {
            _resultViewMono = resultViewMono;
            _disposables.Add(
                Observable
                .EveryValueChanged(this, x => x.IsInResult)
                .Where(isInResult => isInResult)
                .Subscribe(_ =>
            {
                ShowResult();
            }));
            
            _disposables.Add(Observable.EveryUpdate().Subscribe(_ =>
            {
                if (streamTimer.CurrentProgressRatio >= 1)
                {
                    ShowResult(); 
                }
            }));
            
        }
        public void ShowResult()
        {
            IsInResult = true;
            UnityEngine.Time.timeScale = 0;
            _resultViewMono.ShowResult();
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

