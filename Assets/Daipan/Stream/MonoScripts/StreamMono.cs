using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Stream.MonoScripts
{
    public sealed class StreamMono : MonoBehaviour
    {
        ViewerNumber _viewerNumber = null!;
        ViewerParam _viewerParam = null!;
        IrritatedGaugeValue _irritatedGaugeValue = null!;

        float OneSecTimer { get; set; }

        void Update()
        {
            OneSecTimer += Time.deltaTime;
            if (OneSecTimer > 1)
            {
                OneSecTimer = 0;
            }

            // なにもなくても少しづつイライラゲージが貯まる 
            // _irritatedValue.IncreaseValue(1 / 60.0f);  // todo :チュートリアルの関係で一旦なし
            

        }

        [Inject]
        public void Initialize(
            ViewerParam viewerParam,
            ViewerNumber viewerNumber,
            IrritatedGaugeValue irritatedGaugeValue
           ) 
        {
            _viewerParam = viewerParam;
            _viewerNumber = viewerNumber;
            _irritatedGaugeValue = irritatedGaugeValue;
        }
    }
}