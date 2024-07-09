#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Battle.scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.End.MonoScripts
{
    public class EndBackgroundViewMono : MonoBehaviour
    {
        [SerializeField] List<EndSceneSprite> endSceneSprites = null!;
    }

    [Serializable]
    public class EndSceneSprite
    {
        public EndSceneEnum endSceneEnum;
        public Sprite sprite = null!;
    }
}