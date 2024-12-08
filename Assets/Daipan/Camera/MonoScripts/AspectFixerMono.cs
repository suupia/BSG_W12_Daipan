#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Daipan.DaipanCamera.MonoScripts
{
    [ExecuteAlways]
    public class AspectFixerMono : MonoBehaviour
    {
        [SerializeField]
        Vector2 targetResolution = new Vector2(16, 9);
        void Awake()
        {
            var scrnAspect = (float)Screen.width / (float)Screen.height;        // 現在のアスペクト比
            var targetAspect = targetResolution.x / targetResolution.y;           // 目標のアスペクト比

            var rate = targetAspect / scrnAspect;     // 現在と目標との比率
            var rect = new Rect(0, 0, 1, 1);

            // 倍率が小さい場合、横をそろえる
            if (rate < 1)
            {
                rect.width = rate;
                rect.x = 0.5f - rect.width * 0.5f;
            }

            // 縦をそろえる
            else
            {
                rect.height = 1 / rate;
                rect.y = 0.5f - rect.height * 0.5f;
            }

            // 反映
            gameObject.GetComponent<Camera>().rect = rect;
        }

        void Update()
        {
            var scrnAspect = (float)Screen.width / (float)Screen.height;        // 現在のアスペクト比
            var targetAspect = targetResolution.x / targetResolution.y;           // 目標のアスペクト比

            var rate = targetAspect / scrnAspect;     // 現在と目標との比率
            var rect = new Rect(0, 0, 1, 1);

            // 倍率が小さい場合、横をそろえる
            if (rate < 1)
            {
                rect.width = rate;
                rect.x = 0.5f - rect.width * 0.5f;
            }

            // 縦をそろえる
            else
            {
                rect.height = 1 / rate;
                rect.y = 0.5f - rect.height * 0.5f;
            }

            // 反映
            gameObject.GetComponent<Camera>().rect = rect;
        }
    }
}