#nullable enable
using UnityEngine;
using VContainer;
using Daipan.Stream.Scripts;
using R3;
using UnityEngine.UI;

namespace Daipan.Stream.MonoScripts
{
    public sealed class IrritatedGaugeBackgroundViewMono : MonoBehaviour
    {
        [SerializeField] Image IrritatedGuage = null!;

        [Inject]
        public void Initialize(IrritatedValue irritatedValue)
        {
            Observable.EveryValueChanged(irritatedValue, x => irritatedValue.Ratio)
                .Subscribe(_ => UpdateIrritatedGauge(irritatedValue.Ratio))
                .AddTo(this);
        }

        void UpdateIrritatedGauge(double ratio)
        {
            IrritatedGuage.fillAmount = (float)ratio;
        }
    }
}