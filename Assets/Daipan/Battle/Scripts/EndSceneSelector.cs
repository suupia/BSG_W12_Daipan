#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Daipan.End.Scripts;
using Daipan.LevelDesign.EndScene;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Sound.Interfaces;
using Daipan.Sound.MonoScripts;
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
        readonly ISoundManager _soundManager;
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
            , ISoundManager soundManager
        )
        {
            _endSceneTransitionParam = endSceneTransitionParam;
            _viewerNumber = viewerNumber;
            _daipanExecutor = daipanExecutor;
            _comboCounter = comboCounter;
            _soundManager = soundManager;
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
                    _soundManager.FadOutBgm(0.5f);
                    _soundManager.PlaySe(ConvertToSeEnum(judgeSceneName));
                    Debug.Log($"Transit to {judgeSceneName}, ConvertToSeEnum: {ConvertToSeEnum(judgeSceneName)}"); 
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
                EndSceneEnum.NoobGamer => playerMono.Hp.Value / playerMono.MaxHp <=
                                          endSceneTransitionParam.hpPercentThresholdForNoobGamerEnd,
                EndSceneEnum.Seijo => daipanExecutor.DaipanCount <=
                                      endSceneTransitionParam.daipanCountThresholdForSacredLadyEnd,
                EndSceneEnum.ProGamer => counter.MaxComboCount >= 
                                         endSceneTransitionParam.maxComboCountThresholdForProGamerEnd,
                EndSceneEnum.Enjou => daipanExecutor.DaipanCount >=
                                      endSceneTransitionParam.daipanCountThresholdForBacklashEnd,
                EndSceneEnum.Hakononaka => viewerNumber.Number <=
                                             endSceneTransitionParam.viewerCountThresholdForInsideTheBoxEnd,
                EndSceneEnum.Kansyasai => viewerNumber.Number >=
                                             endSceneTransitionParam.viewerCountThresholdForThanksgivingEnd,
                EndSceneEnum.Heibon => endSceneTransitionParam.viewerCountThresholdForHeibonEndMin <= viewerNumber.Number &&
                                        viewerNumber.Number <= endSceneTransitionParam.viewerCountThresholdForHeibonEndMax,
                EndSceneEnum.Genkai => true,
                _ => false
            };
            Debug.Log($"TransitionCondition() SceneName: {sceneName}, result : {result}, viewerNumber: {viewerNumber.Number}, hp: {playerMono.Hp.Value}, maxHp: {playerMono.MaxHp}, daipanCount: {daipanExecutor.DaipanCount}");
            if (!result) Debug.LogWarning($"TransitionCondition is not satisfied: {sceneName}");
            return result;
        }
        public static SeEnum ConvertToSeEnum(EndSceneEnum endScene)
        {
            switch (endScene)
            {
                case EndSceneEnum.Hakononaka:
                    return SeEnum.Hakononaka;
                case EndSceneEnum.Kansyasai:
                    return SeEnum.Kansyasai;
                case EndSceneEnum.NoobGamer:
                    return SeEnum.NoobGamer;
                case EndSceneEnum.ProGamer:
                    return SeEnum.ProGamer;
                case EndSceneEnum.Seijo:
                    return SeEnum.Seijo;
                case EndSceneEnum.Enjou:
                    return SeEnum.Enjou;
                case EndSceneEnum.Genkai:
                    return SeEnum.Genkai;
                case EndSceneEnum.Heibon:
                    return SeEnum.Heibon;
                default:
                    throw new ArgumentOutOfRangeException(nameof(endScene), endScene, null);
            }
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