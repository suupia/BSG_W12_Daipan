#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.scripts;
using Daipan.Enemy.Scripts;
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
            WaveProgress waveProgress
            , ResultViewMono resultViewMono
            , EnemyCluster enemyCluster
        )
        {
            _resultViewMono = resultViewMono;

            _disposables.Add(Observable.EveryUpdate().Subscribe(_ =>
            {
                if (waveProgress.CurrentProgressRatio >= 1 && !enemyCluster.Enemies.Any())
                { 
                    const double delaySec = 2;
                    _disposables.Add(
                        Observable
                            .Timer(TimeSpan.FromSeconds(delaySec))
                            .Subscribe(_ => ShowResult())
                        );
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

