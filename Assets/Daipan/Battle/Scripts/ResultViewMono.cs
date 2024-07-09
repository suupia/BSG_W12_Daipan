#nullable enable
using System;
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts;
using R3;
using TMPro;
using UnityEngine;
using VContainer;

namespace Daipan.Battle.scripts
{
    public class ResultViewMono : MonoBehaviour
    {
        [SerializeField] GameObject viewObject = null!;
        [SerializeField] TextMeshProUGUI viewerNumberText = null!;
        [SerializeField] TextMeshProUGUI daipanCountText = null!;
        [SerializeField] TextMeshProUGUI playerHpText = null!;
        [SerializeField] TextMeshProUGUI tankYouText = null!;
        [SerializeField] TextMeshProUGUI pushEnterText = null!;

        void Awake()
        {
            HideResult(); 
        }

        [Inject]
        public void Constructor(
            ViewerNumber viewerNumber
            , DaipanExecutor daipanExecutor
            )
        {
            Debug.Log("ResultViewMono Constructor");
            Observable.EveryValueChanged(viewerNumber, x => x.Number)
                .Where(_ => viewObject.activeSelf)
                .Subscribe(x => viewerNumberText.text = x.ToString())
                .AddTo(this);
            Observable.EveryValueChanged(daipanExecutor, x => x.DaipanCount)
                .Where(_ => viewObject.activeSelf)
                .Subscribe(x => daipanCountText.text = x.ToString())
                .AddTo(this);
        }
        
        public void ShowResult()
        {
            var playerMono = UnityEngine.Object.FindObjectOfType<PlayerMono>();
            if (playerMono == null)
            {
                Debug.LogWarning("PlayerMono is not found");
                return;
            }
            playerHpText.text = playerMono.Hp.Value.ToString();  // 本当はObserveしたいけど生成順序の関係でここで取得
            viewObject.SetActive(true);
        }
        
        public void HideResult()
        {
            viewObject.SetActive(false);
        }
    }
}