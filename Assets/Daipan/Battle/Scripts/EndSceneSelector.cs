#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Core.Interfaces;
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
        readonly List<SceneName> _judgeList = new()
        {
            SceneName.SacredLady,
            SceneName.Backlash,
            SceneName.NoobGamer,
            SceneName.ProGamer,
            SceneName.InsideTheBox,
            SceneName.Thanksgiving,
            SceneName.StrugglingStreamer
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
            {
                if (TransitionCondition(judgeSceneName, _viewerNumber, _playerMono!.Hp, _daipanExecutor))
                {
                    ResultShower.ShowResult(judgeSceneName);
                    break;
                }
            }
            Debug.LogWarning("No scene to transit");
        }

        static bool TransitionCondition(
            SceneName sceneName
            , ViewerNumber viewerNumber
            , Hp hp
            , DaipanExecutor daipanExecutor
        )
        {
            var result = sceneName switch
            {
                SceneName.InsideTheBox => viewerNumber.Number <= 500,
                SceneName.Thanksgiving => viewerNumber.Number >= 1000,
                SceneName.NoobGamer => hp.Value <= 0,
                SceneName.ProGamer => hp.Value >= 50,
                SceneName.SacredLady => daipanExecutor.DaipanCount <= 10,
                SceneName.Backlash => daipanExecutor.DaipanCount >= 10,
                SceneName.StrugglingStreamer => true,
                _ => false
            };
            if (!result) Debug.LogWarning($"TransitionCondition is not satisfied: {sceneName}");
            return result;
        }

    }
}