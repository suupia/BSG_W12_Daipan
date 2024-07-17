#nullable enable
using System;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
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
        [SerializeField] TextMeshProUGUI viewerNumberExplainText = null!;
        [SerializeField] TextMeshProUGUI viewerNumberText = null!;
        [SerializeField] TextMeshProUGUI daipanCountExplainText = null!;
        [SerializeField] TextMeshProUGUI daipanCountText = null!;
        [SerializeField] TextMeshProUGUI playerHpExplainText = null!;
        [SerializeField] TextMeshProUGUI playerHpText = null!;
        [SerializeField] TextMeshProUGUI comboCountExplainText = null!;
        [SerializeField] TextMeshProUGUI comboCountText = null!;
        [SerializeField] TextMeshProUGUI tankYouText = null!;
        [SerializeField] TextMeshProUGUI pushEnterText = null!;

        void Awake()
        {
            HideResult(); 
        }

        [Inject]
        public void Constructor(
            ViewerNumber viewerNumber
            , ComboCounter comboCounter
            , DaipanExecutor daipanExecutor
            )
        {
            Debug.Log("ResultViewMono Constructor");
            Observable.EveryUpdate()
                .Where(_ => viewObject.activeInHierarchy)
                .Subscribe(_ => viewerNumberText.text = $"{viewerNumber.Number}")
                .AddTo(this);
            Observable.EveryUpdate()
                .Where(_ => viewObject.activeInHierarchy)
                .Subscribe(_ => daipanCountText.text = $"{daipanExecutor.DaipanCount}")
                .AddTo(this);
            Observable.EveryUpdate()
                .Where(_ => viewObject.activeInHierarchy)
                .Subscribe(_ => comboCountText.text = $"{comboCounter.MaxComboCount}")
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
            playerHpText.text = $"{playerMono.Hp.Value} / {playerMono.MaxHp}";  // 本当はObserveしたいけど生成順序の関係でここで取得

            viewerNumberExplainText.text = "同時接続数";
            daipanCountExplainText.text = "台パン回数";
            playerHpExplainText.text = "塔の最終のHP";
            comboCountExplainText.text = "最大コンボ数";
            
            tankYouText.text = "ご視聴いただきありがとうございました！";
            pushEnterText.text = "次へ"; 
            
            viewObject.SetActive(true);
        }
        
        public void HideResult()
        {
            viewObject.SetActive(false);
        }
    }
}