#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Daipan.Stream.MonoScripts
{
    public sealed class DigitSplitViewMono : MonoBehaviour
    {
        [SerializeField] List<TextMeshProUGUI> digitTexts = new();

        public void SetDigit(int digit)
        {
            var digitString = digit.ToString().Reverse().ToArray();

            if (digitTexts.Count < digitString.Length)
            {
                Debug.LogWarning(
                    $"digitTexts.Count < digitString.Length. digitTexts.Count: {digitTexts.Count}, digitString.Length: {digitString.Length}");
                return;
            }

            for (var i = 0; i < digitTexts.Count; i++)
                if (i < digitString.Length)
                    digitTexts[i].text = digitString[i].ToString();
                else
                    digitTexts[i].text = "";
        }
    }
}