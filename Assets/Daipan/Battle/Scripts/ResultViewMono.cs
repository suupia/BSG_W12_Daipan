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
            Observable.EveryUpdate()
                .Where(_ => viewObject.activeInHierarchy)
                .Subscribe(_ => viewerNumberText.text = $"Viewer Number: {viewerNumber.Number}")
                .AddTo(this);
            Observable.EveryUpdate()
                .Where(_ => viewObject.activeInHierarchy)
                .Subscribe(_ => daipanCountText.text = $"Daipan Count: {daipanExecutor.DaipanCount}")
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
            playerHpText.text = $"Player HP :{playerMono.Hp.Value}";  // 本当はObserveしたいけど生成順序の関係でここで取得
            
            tankYouText.text = "Thank you for playing!";
            pushEnterText.text = "Push Enter to see the end scene"; 
            
            viewObject.SetActive(true);
        }
        
        public void HideResult()
        {
            viewObject.SetActive(false);
        }
    }
}