#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Daipan.Effects.MonoScripts
{
    public class FrameViewMono : MonoBehaviour
    {
        [SerializeField] Image originalFrame;
        [SerializeField] Image antiFrame;
        [SerializeField] Image maxAntiFrame;

        [SerializeField] Transform antiCommentParent;
        
        void Start()
        {

        }

        
        void Update()
        {
            int antiCommentNum = antiCommentParent.childCount;
            //Debug.Log($"antiCommentNum:{antiCommentNum}");
        }
    }
}