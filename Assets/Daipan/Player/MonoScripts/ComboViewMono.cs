#nullable enable
using Daipan.Player.Scripts;
using TMPro;
using UnityEngine;
using VContainer;
using R3;

namespace Daipan.Player.MonoScripts
{
    public class ComboViewMono : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI comboText = null!;

        [Inject]
        public void Initialize(ComboCounter comboCounter)
        {
            Observable.EveryValueChanged(comboCounter, x => x.ComboCount)
                .Subscribe(_ => UpdateComboText(comboCounter.ComboCount));
        }

        void UpdateComboText(int comboCount)
        {
            comboText.text = $"{comboCount}";
        }
    }
}