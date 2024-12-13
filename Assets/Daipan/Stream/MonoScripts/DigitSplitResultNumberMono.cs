#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Daipan.Stream.MonoScripts
{
    public sealed class DigitSplitResultNumberMono : MonoBehaviour
    {
        [SerializeField] DigitSpritesMono digitSpritesMono = new(); // 0~ 9までの画像
        [SerializeField] List<Image> digitSprites = new(); // 桁を表す画像

        public void SetDigit(int digit)
        {
            var digitString = digit.ToString().Reverse().ToArray();

            if (digitSprites.Count < digitString.Length)
            {
                Debug.LogWarning(
                    $"digitSprites.Count < digitSprites.Length. digitSprites.Count: {digitSprites.Count}, digitSprites.Length: {digitString.Length}");
                return;
            }

            for (var i = 0; i < digitSprites.Count; i++)
                if (i < digitString.Length)
                {
                    digitSprites[i].sprite = digitSpritesMono[int.Parse(digitString[i].ToString())];
                }
                else
                {
                    digitSprites[i].sprite = null;
                }
        }
    }
}