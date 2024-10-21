#nullable enable
using UnityEngine;
using VContainer;
using Daipan.Stream.Scripts;
using R3;
using UnityEngine.UI;

namespace Daipan.Stream.MonoScripts
{
    public sealed class IrritatedViewMono : MonoBehaviour
    {
        [SerializeField] Image IrritatedGuage = null!;

        [Inject]
        public void Initialize(IrritatedGaugeValue irritatedGaugeValue)
        {
            Observable.EveryValueChanged(irritatedGaugeValue, x => irritatedGaugeValue.Ratio)
                .Subscribe(_ => UpdateIrritatedGauge(irritatedGaugeValue.Ratio))
                .AddTo(this);
        }

        void UpdateIrritatedGauge(double ratio)
        {
            IrritatedGuage.fillAmount = (float)ratio;
        }
    }
}