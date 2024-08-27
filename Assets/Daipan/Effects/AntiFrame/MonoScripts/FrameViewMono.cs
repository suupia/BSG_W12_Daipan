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

        [Header("アンチコメントがn以上あれば、antiFrame")]
        [Min(0)]
        [SerializeField] int antiThreshold;

        [Header("アンチコメントがn以上あれば、maxAntiFrame")]
        [Min(0)]
        [SerializeField] int maxAntiThreshold;


        int preAntiCommentNum = 0;

        void Start()
        {

        }

        
        void Update()
        {
            if (antiThreshold >= maxAntiThreshold) Debug.LogError("antiThreshold >= maxAntiThreshold は不正値です。");
            int antiCommentNum = antiCommentParent.childCount;
            //Debug.Log($"antiCommentNum:{antiCommentNum}");

            if (preAntiCommentNum == antiCommentNum) return;

            if(antiCommentNum < antiThreshold)
            {
                ShowOriginalFrame();
            }
            else if(antiCommentNum < maxAntiThreshold)
            {
                ShowAntiFrame();
            }
            else
            {
                ShowMaxAntiFrame();
            }


            preAntiCommentNum = antiCommentNum;
        }

        void ShowOriginalFrame()
        {
            originalFrame.color = Color.white;
            antiFrame.enabled = false;
            maxAntiFrame.enabled = false;
        }
        void ShowAntiFrame()
        {
            originalFrame.color = new Vector4(0.8f, 0.8f, 0.8f, 1f);
            antiFrame.enabled = true;
            maxAntiFrame.enabled = false;
        }
        void ShowMaxAntiFrame()
        {
            originalFrame.color = new Vector4(0.8f, 0.8f, 0.8f, 1f);
            antiFrame.enabled = true;
            maxAntiFrame.enabled = true;
        }

    }
}