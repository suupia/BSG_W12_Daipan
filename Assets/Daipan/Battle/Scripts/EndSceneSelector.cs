#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Daipan.End.Scripts;
using Daipan.LevelDesign.EndScene;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;
using R3;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Daipan.Battle.scripts
{
    public sealed class EndSceneSelector : IDisposable
    {
        readonly EndSceneTransitionParam _endSceneTransitionParam;
        readonly ViewerNumber _viewerNumber;
        readonly DaipanExecutor _daipanExecutor;
        readonly StreamTimer _streamTimer;
        IDisposable? _disposable;

        // この順番でシーン遷移の判定を行っていく
        readonly List<EndSceneEnum> _judgeList = new()
        {
            EndSceneEnum.SacredLady,
            EndSceneEnum.Backlash,
            EndSceneEnum.NoobGamer,
            EndSceneEnum.ProGamer,
            EndSceneEnum.InsideTheBox,
            EndSceneEnum.Thanksgiving,
            EndSceneEnum.StrugglingStreamer
        };

        public EndSceneSelector(
            EndSceneTransitionParam endSceneTransitionParam
            , ViewerNumber viewerNumber
            , DaipanExecutor daipanExecutor
            , StreamTimer streamTimer
        )
        {
            _endSceneTransitionParam = endSceneTransitionParam;
            _viewerNumber = viewerNumber;
            _daipanExecutor = daipanExecutor;
            _streamTimer = streamTimer;
            
            ObserveStreamTimer();
            // 本当はPlayerのHpもここでObserveしたいが、PlayerのHpの初期化が遅いので、PlayerMonoに書いている
        }
        
        void ObserveStreamTimer()
        {
             _disposable = Observable.EveryUpdate ()
                .Subscribe (_ =>
                {
                    if (_streamTimer.CurrentProgressRatio >= 1) TransitToEndScene();
                });
        }


        public void TransitToEndScene()
        {
            var playerMono = UnityEngine.Object.FindObjectOfType<PlayerMono>();
            if (playerMono == null)
            {
                Debug.LogWarning("PlayerMono is not found");
                return;
            }

            foreach (var judgeSceneName in _judgeList)
                if (TransitionCondition(judgeSceneName, _endSceneTransitionParam, _viewerNumber, playerMono, _daipanExecutor))
                {
                    EndSceneStatic.EndSceneEnum = judgeSceneName;
                    SceneTransition.TransitioningScene(SceneName.EndScene);
                    return;
                }

            Debug.LogWarning("No scene to transit");
        }

        static bool TransitionCondition(
            EndSceneEnum sceneName
            , EndSceneTransitionParam endSceneTransitionParam
            , ViewerNumber viewerNumber
            , PlayerMono playerMono
            , DaipanExecutor daipanExecutor
        )
        {
            var result = sceneName switch
            {
                EndSceneEnum.InsideTheBox => viewerNumber.Number <=
                                             endSceneTransitionParam.viewerCountThresholdForInsideTheBoxEnd,
                EndSceneEnum.Thanksgiving => viewerNumber.Number >=
                                             endSceneTransitionParam.viewerCountThresholdForThanksgivingEnd,
                EndSceneEnum.NoobGamer => (double)playerMono.Hp.Value / playerMono.MaxHp <=
                                          endSceneTransitionParam.hpPercentThresholdForNoobGamerEnd,
                EndSceneEnum.ProGamer => (double)playerMono.Hp.Value / playerMono.MaxHp <=
                                         endSceneTransitionParam.hpPercentThresholdForProGamerEnd,
                EndSceneEnum.SacredLady => daipanExecutor.DaipanCount <=
                                           endSceneTransitionParam.daipanCountThresholdForSacredLadyEnd,
                EndSceneEnum.Backlash => daipanExecutor.DaipanCount >=
                                         endSceneTransitionParam.daipanCountThresholdForBacklashEnd,
                EndSceneEnum.StrugglingStreamer => true,
                _ => false
            };
            Debug.Log($"TransitionCondition() SceneName: {sceneName}, result : {result}, viewerNumber: {viewerNumber.Number}, hp: {playerMono.Hp.Value}, maxHp: {playerMono.MaxHp}, daipanCount: {daipanExecutor.DaipanCount}");
            if (!result) Debug.LogWarning($"TransitionCondition is not satisfied: {sceneName}");
            return result;
        }
        public void Dispose()
        {
            _disposable?.Dispose();
        }
        ~EndSceneSelector()
        {
            Dispose();
        }
    }

    public enum EndSceneEnum
    {
        InsideTheBox,
        Thanksgiving,
        NoobGamer,
        ProGamer,
        SacredLady,
        Backlash,
        StrugglingStreamer
    }
}