#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Tutorial.Scripts;
using TMPro;
using UnityEngine;
using DG.Tweening;
using R3;
using UnityEngine.Serialization;
using VContainer;

namespace Daipan.Streamer.Scripts
{
    public class PushEnterTextMono : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI pushEnterText = null!;

        const float DurationSec = 0.5f;
        double Timer { get; set; }
        const double MinShowSec = 0.5; 
        readonly Queue<string> _speechQueue = new ();



        public void Show()
        {
             
        }

        public void Hide()
        {
            
        }
    }
    
}