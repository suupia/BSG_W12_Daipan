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

namespace Daipan.Result.MonoScripts
{
    public class ResultMonoOnline : MonoBehaviour
    {
        [SerializeField] Image resultImage = null!;
        [SerializeField] ResultSpriteParam[] resultSpriteParams = null!; 
        
        [SerializeField] TMP_Text resultText = null!;

        void Start()
        {
            resultText.text = NetworkPlayerResultHolder.NetworkPlayerResultEnum.ToString();

            // 念のため、残っている敵を削除
            var remainingEnemies = FindObjectsByType<EnemyNet>(FindObjectsSortMode.None);
            foreach (var enemy in remainingEnemies)
                enemy?.DeleteSelf();
            var remainingFinalBosses = FindObjectsByType<FinalBossNet>(FindObjectsSortMode.None);
            foreach (var finalBoss in remainingFinalBosses)
                finalBoss?.DeleteSelf();
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
        public Sprite? resultSprite;
    }

    public static class NetworkPlayerResultHolder
    {
        public static NetworkPlayerResultEnum NetworkPlayerResultEnum { get; set; } = NetworkPlayerResultEnum.None;
    }

    public enum NetworkPlayerResultEnum
    {
        None,
        Win,
        Lose,
        QuitByPlayerLeft,
    }
}