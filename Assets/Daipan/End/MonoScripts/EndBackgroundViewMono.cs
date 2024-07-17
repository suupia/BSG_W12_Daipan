#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.scripts;
using Daipan.End.Scripts;
using Daipan.Option.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VContainer;

namespace Daipan.End.MonoScripts
{
    public class EndBackgroundViewMono : MonoBehaviour
    {
        [SerializeField] List<EndSceneSprite> endSceneSprites = null!;
        
        [SerializeField] Image endSceneImage = null!;
        [SerializeField] TextMeshProUGUI endSceneText = null!; // フレーバーテキストを表示

        void Awake()
        {
            endSceneImage.sprite = endSceneSprites
                .Where(x => x.endSceneEnum == EndSceneStatic.EndSceneEnum)
                .Select(x => x.sprite).FirstOrDefault();
            
            endSceneText.text =  GetEndSceneText(EndSceneStatic.EndSceneEnum); 
        }
        
        static string GetEndSceneText(EndSceneEnum endSceneEnum)
        {
            return endSceneEnum switch
            {
                EndSceneEnum.Seijo => "聖女END",
                EndSceneEnum.Enjou => "炎上END",
                EndSceneEnum.NoobGamer => "ゲーム下手配信者END",
                EndSceneEnum.ProGamer => "プロゲーマー配信者END",
                EndSceneEnum.Hakononaka => "箱の中END",
                EndSceneEnum.Kansyasai => "感謝祭END",
                EndSceneEnum.Genkai => "限界配信者END",
                EndSceneEnum.Heibon => "平凡END",
                _ => throw new ArgumentOutOfRangeException(nameof(endSceneEnum), endSceneEnum, null)
            };
        }
    }

    [Serializable]
    public class EndSceneSprite
    {
        public EndSceneEnum endSceneEnum;
        public Sprite sprite = null!;
    }
}