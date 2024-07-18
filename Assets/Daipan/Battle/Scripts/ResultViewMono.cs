#nullable enable
using System;
using Daipan.Option.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
        
        [SerializeField] Sprite successBackground = null!;
        [SerializeField] Sprite failureBackground = null!;
        [SerializeField] Image background = null!;

        LanguageConfig _languageConfig = null!;
        void Awake()
        {
            HideResult(); 
        }

        [Inject]
        public void Constructor(
            ViewerNumber viewerNumber
            , ComboCounter comboCounter
            , DaipanExecutor daipanExecutor
            , LanguageConfig languageConfig
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
            
            _languageConfig = languageConfig;
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

            if (playerMono.Hp.Value <= 0)
            {
                background.sprite = failureBackground;
            }
            else
            {
                background.sprite = successBackground;
            }

            viewerNumberExplainText.text = _languageConfig.CurrentLanguage switch
            {
                LanguageEnum.Japanese => "同時接続数",
                LanguageEnum.English => "Concurrent Connections",
                _ => throw new ArgumentOutOfRangeException()
            };

            daipanCountExplainText.text = _languageConfig.CurrentLanguage switch
            {
                LanguageEnum.Japanese => "台パン回数",
                LanguageEnum.English => "Number of Daipan",
                _ => throw new ArgumentOutOfRangeException()
            };

            playerHpExplainText.text = _languageConfig.CurrentLanguage switch
            {
                LanguageEnum.Japanese => "塔の最終HP",
                LanguageEnum.English => "Final HP",
                _ => throw new ArgumentOutOfRangeException()
            };

            comboCountExplainText.text = _languageConfig.CurrentLanguage switch
            {
                LanguageEnum.Japanese => "最大コンボ数",
                LanguageEnum.English => "Max Combo Count",
                _ => throw new ArgumentOutOfRangeException()
            };
            
            tankYouText.text = _languageConfig.CurrentLanguage switch
            {
                LanguageEnum.Japanese => "ご視聴ありがとうございました！",
                LanguageEnum.English => "Thank you for watching!",
                _ => throw new ArgumentOutOfRangeException()
            };
            pushEnterText.text = _languageConfig.CurrentLanguage switch
            {
                LanguageEnum.Japanese => "次へ",
                LanguageEnum.English => "Next",
                _ => throw new ArgumentOutOfRangeException()
            }; 
            
            viewObject.SetActive(true);
        }
        
        public void HideResult()
        {
            viewObject.SetActive(false);
        }
    }
}