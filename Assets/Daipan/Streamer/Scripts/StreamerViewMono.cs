#nullable enable
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Streamer.Scripts
{
    public class StreamerViewMono : MonoBehaviour
    {
        [SerializeField] Animator animator = null!;

        IrritatedValue _irritatedValue = null!;

        [Inject]
        void Initialize(IrritatedValue irritatedValue)
        {
            _irritatedValue = irritatedValue;
        }

        void Update()
        {
            animator.SetInteger("IrritatedStage", _irritatedValue.CurrentIrritatedStage);
            //Debug.Log($"CurrentIrritatedStage : {_irritatedValue.CurrentIrritatedStage}");
        }

        public void Daipan()
        {
            animator.SetBool("IsDaipan", true);
        }
    }
}
