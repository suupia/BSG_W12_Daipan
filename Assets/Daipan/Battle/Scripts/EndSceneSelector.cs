#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Daipan.End.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;
using R3;
using UnityEngine;

namespace Daipan.Battle.scripts
{
    public sealed class EndSceneSelector
    {
        readonly ViewerNumber _viewerNumber;
        readonly DaipanExecutor _daipanExecutor;

        PlayerMono? _playerMono;


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
            ViewerNumber viewerNumber
            , DaipanExecutor daipanExecutor
        )
        {
            _viewerNumber = viewerNumber;
            _daipanExecutor = daipanExecutor;
        }


        public void TransitToEndScene()
        {
            foreach (var judgeSceneName in _judgeList)
                if (TransitionCondition(judgeSceneName, _viewerNumber, _playerMono!.Hp, _daipanExecutor))
                {
                    EndSceneStatic.EndSceneEnum = judgeSceneName;
                    SceneTransition.TransitioningScene(SceneName.EndScene);
                    break;
                }

            Debug.LogWarning("No scene to transit");
        }

        static bool TransitionCondition(
            EndSceneEnum sceneName
            , ViewerNumber viewerNumber
            , Hp hp
            , DaipanExecutor daipanExecutor
        )
        {
            var result = sceneName switch
            {
                EndSceneEnum.InsideTheBox => viewerNumber.Number <= 500,
                EndSceneEnum.Thanksgiving => viewerNumber.Number >= 1000,
                EndSceneEnum.NoobGamer => hp.Value <= 0,
                EndSceneEnum.ProGamer => hp.Value >= 50,
                EndSceneEnum.SacredLady => daipanExecutor.DaipanCount <= 10,
                EndSceneEnum.Backlash => daipanExecutor.DaipanCount >= 10,
                EndSceneEnum.StrugglingStreamer => true,
                _ => false
            };
            if (!result) Debug.LogWarning($"TransitionCondition is not satisfied: {sceneName}");
            return result;
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