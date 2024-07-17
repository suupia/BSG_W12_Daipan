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
        readonly ComboCounter _comboCounter;
        IDisposable? _disposable;

        // この順番でシーン遷移の判定を行っていく
        readonly List<EndSceneEnum> _judgeList = new()
        {
            EndSceneEnum.Seijo,
            EndSceneEnum.Enjou,
            EndSceneEnum.NoobGamer,
            EndSceneEnum.ProGamer,
            EndSceneEnum.Hakononaka,
            EndSceneEnum.Kansyasai,
            EndSceneEnum.Genkai
        };

        public EndSceneSelector(
            EndSceneTransitionParam endSceneTransitionParam
            , ViewerNumber viewerNumber
            , DaipanExecutor daipanExecutor
            , ComboCounter comboCounter
        )
        {
            _endSceneTransitionParam = endSceneTransitionParam;
            _viewerNumber = viewerNumber;
            _daipanExecutor = daipanExecutor;
            _comboCounter = comboCounter;
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
                if (TransitionCondition(judgeSceneName, _endSceneTransitionParam, _viewerNumber, playerMono, _daipanExecutor, _comboCounter))
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
            , ComboCounter counter
        )
        {
            var result = sceneName switch
            {
                EndSceneEnum.Hakononaka => viewerNumber.Number <=
                                             endSceneTransitionParam.viewerCountThresholdForInsideTheBoxEnd,
                EndSceneEnum.Kansyasai => viewerNumber.Number >=
                                             endSceneTransitionParam.viewerCountThresholdForThanksgivingEnd,
                EndSceneEnum.NoobGamer => playerMono.Hp.Value / playerMono.MaxHp <=
                                          endSceneTransitionParam.hpPercentThresholdForNoobGamerEnd,
                EndSceneEnum.ProGamer => counter.MaxComboCount >= 
                                        endSceneTransitionParam.maxComboCountThresholdForProGamerEnd,
                EndSceneEnum.Seijo => daipanExecutor.DaipanCount <=
                                           endSceneTransitionParam.daipanCountThresholdForSacredLadyEnd,
                EndSceneEnum.Enjou => daipanExecutor.DaipanCount >=
                                         endSceneTransitionParam.daipanCountThresholdForBacklashEnd,
                EndSceneEnum.Genkai => true,
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
        Hakononaka,  // 箱の中END
        Kansyasai,  // 配信者ちゃん感謝祭END
        NoobGamer,     // ゲーム下手配信者END
        ProGamer,      // プロゲーマーEND
        Seijo,    // 聖女END
        Enjou,      // 炎上END
        Genkai, // 限界配信者END
        Heibon, // 平凡な配信者END
    }
}