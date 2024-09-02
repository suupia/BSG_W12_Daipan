#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.scripts;
using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts;
using R3;
using UnityEngine;

namespace Daipan.Battle.Scripts
{
    public class ResultState : IDisposable
    {
        readonly ResultViewMono _resultViewMono;
        readonly List<IDisposable> _disposables = new();
        
        public enum ResultEnum
        {
            None, 
            Result, // 配信終了
            Details, // 詳細
        }
        public ResultEnum CurrentResultEnum { get; private set; } = ResultEnum.None;

        public ResultState(
            WaveProgress waveProgress
            , ResultViewMono resultViewMono
            , FinalBossDefeatTracker finalBossDefeatTracker
        )
        {
            _resultViewMono = resultViewMono;

            _disposables.Add(Observable.EveryUpdate()
                .Where(_ => CurrentResultEnum == ResultEnum.None)
                .Subscribe(_ =>
                {
                    if (waveProgress.CurrentProgressRatio >= 1 && finalBossDefeatTracker.IsFinalBossDefeated &&
                        CurrentResultEnum == ResultEnum.None)
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
            _resultViewMono.ShowResult(onComplete:()=>CurrentResultEnum = ResultEnum.Result);
        }
        
        public void ShowDetails(){ 
            CurrentResultEnum = ResultEnum.Details;
            Debug.Log($"ShowDetails CurrentResultEnum:{CurrentResultEnum}");
            _resultViewMono.ShowDetails();
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

