#nullable enable
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;
using Daipan.Stream.Scripts;

namespace Daipan.Streamer.Scripts
{
    public class StreamerViewMono : MonoBehaviour
    {
        [SerializeField] Animator animator = null!;

        IrritatedValue _irritatedValue;


        [Inject]
        void Initialize(IrritatedValue irritatedValue)
        {
            _irritatedValue = irritatedValue;
        }

        
        private void Awake()
        {
            if (animator == null) Debug.LogWarning("animator is null");
        }
        void Update()
        {
            animator.SetInteger("IrritatedPhase", _irritatedValue.GetIrritatedPhase());
        }
    }
}
