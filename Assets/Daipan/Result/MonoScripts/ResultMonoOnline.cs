#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;
using Daipan.Battle.scripts;
using Daipan.Enemy.MonoScripts;
using Fusion;
using System.Linq;
using UnityEngine.UI;
using Daipan.Comment.MonoScripts;

namespace Daipan.Result.MonoScripts
{
    public class ResultMonoOnline : MonoBehaviour
    {
        [SerializeField] Image resultImage = null!;
        [SerializeField] Image quiteImage = null!;
        [SerializeField] ResultSpriteParam[] resultSpriteParams = null!;
        [SerializeField] ResultQuitImage[] resultQuitImages = null!;

        [SerializeField] TMP_Text resultText = null!;

        void Start()
        {
            resultText.text = NetworkPlayerResultHolder.NetworkPlayerResultEnum.ToString();

            // Resultの結果を表す画像
            foreach (var resultSpriteParam in resultSpriteParams)
            {
                if (resultSpriteParam.networkPlayerResultEnum == NetworkPlayerResultHolder.NetworkPlayerResultEnum)
                {
                    resultImage.sprite = resultSpriteParam.resultSprite;
                }
            }

            // 「終了」ボタンの画像
            foreach (var resultQuitImage in resultQuitImages)
            {
                if (resultQuitImage.networkPlayerResultEnum == NetworkPlayerResultHolder.NetworkPlayerResultEnum)
                {
                    quiteImage.sprite = resultQuitImage.quitSprite;
                }
            }

            // 念のため、残っている敵を削除
            var remainingEnemies = FindObjectsByType<EnemyNet>(FindObjectsSortMode.None);
            foreach (var enemy in remainingEnemies)
                enemy?.DeleteSelf();
            var remainingFinalBosses = FindObjectsByType<FinalBossNet>(FindObjectsSortMode.None);
            foreach (var finalBoss in remainingFinalBosses)
                finalBoss?.DeleteSelf();
            // アンチコメントも削除
            var remainingAntiComments = FindObjectsByType<AntiCommentNet>(FindObjectsSortMode.None);
            foreach (var antiComment in remainingAntiComments)
                antiComment?.DeleteSelf();
            var remainingSpecialAntiComments = FindObjectsByType<AntiCommentNet>(FindObjectsSortMode.None);
            foreach (var specialAntiComment in remainingSpecialAntiComments)
                specialAntiComment?.DeleteSelf();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneTransition.TransitioningScene(SceneName.TitleSceneNet);
            }
        }
    }

    [Serializable]
    public sealed class ResultSpriteParam
    {
        public NetworkPlayerResultEnum networkPlayerResultEnum;
        public Sprite? resultSprite;  // 勝敗に応じて表示する画像
    }

    [Serializable]
    public sealed class ResultQuitImage
    {
        public NetworkPlayerResultEnum networkPlayerResultEnum;
        public Sprite? quitSprite;  // ResultSpriteに応じて表示
    }

    public static class NetworkPlayerResultHolder
    {
        public static NetworkPlayerResultEnum NetworkPlayerResultEnum { get; set; } = NetworkPlayerResultEnum.None;
    }

    public enum NetworkPlayerResultEnum
    {
        None,
        StreamerWin,
        AntiWin,
        QuitByPlayerLeft,
    }
}