#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.scripts;
using Daipan.End.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Daipan.End.MonoScripts
{
    public class EndBackgroundViewMono : MonoBehaviour
    {
        [SerializeField] List<EndSceneSprite> endSceneSprites = null!;
        
        [SerializeField] Image endSceneImage = null!;
        [SerializeField] TextMeshProUGUI endSceneText = null!; // todo: debug用にEndSceneEnumを表示する

        void Awake()
        {
            endSceneImage.sprite = endSceneSprites
                .Where(x => x.endSceneEnum == EndSceneStatic.EndSceneEnum)
                .Select(x => x.sprite).FirstOrDefault();
            
            endSceneText.text = EndSceneStatic.EndSceneEnum.ToString();
        }
    }

    [Serializable]
    public class EndSceneSprite
    {
        public EndSceneEnum endSceneEnum;
        public Sprite sprite = null!;
    }
}