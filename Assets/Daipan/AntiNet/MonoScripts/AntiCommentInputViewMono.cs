#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.AntiNet.Scripts;
using TMPro;
using UnityEngine;
using VContainer;
using R3;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Net;
using UnityEngine.UI;

namespace Daipan.AntiNet.MonoScripts
{
    public class AntiCommentInputViewMono : MonoBehaviour
    {
        [SerializeField] Image writingImage = null!;
        [SerializeField] List<TMP_Text> characterTexts = null!;

        public void UpdateCharacters(string characters)
        {
            for (int i = 0; i < characterTexts.Count; i++)
            {
                if (i >= characters.Length) characterTexts[i].text = "";
                else characterTexts[i].text = $"{characters[i]}";
            }
        }
        public void OpenChat()
        {
            writingImage.gameObject.SetActive(true);
        }
        public void CloseChat()
        {
            writingImage.gameObject.SetActive(false);
        }
    }
}