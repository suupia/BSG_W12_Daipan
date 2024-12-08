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
    public class CostViewMono : MonoBehaviour
    {
        [SerializeField] TMP_Text costText = null!;
        [SerializeField] TMP_Text maxCostText = null!;


        [Inject]
        public void Initialize(
            SpawnEnemyCostValue cost
            , ICostValueParam costParam)
        {
            Observable
                .EveryValueChanged(cost, x => x.Value)
                .Subscribe(value =>
                {
                    costText.text = value.ToString();
                })
                .AddTo(this);

            maxCostText.text = $"/ {costParam.MaxCostValue}";
        }
    }
}