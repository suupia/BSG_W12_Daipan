#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.AntiNet.Scripts;
using TMPro;
using UnityEngine;
using VContainer;
using R3;

namespace Daipan.AntiNet.MonoScripts
{
    public class AntiStateViewMono : MonoBehaviour
    {
        [SerializeField] TMP_Text stateText = null!;


        [Inject]
        public void Initialize(AntiStateValue stateValue)
        {
            Observable
                .EveryValueChanged(stateValue, x => x.AntiStateEnum)
                .Subscribe(value =>
                {
                    stateText.text = value.ToString();
                })
                .AddTo(this);
        }
    }
}