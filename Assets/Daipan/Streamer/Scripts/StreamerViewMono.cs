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
            animator.SetBool("IsAngry", _irritatedValue.IsFull);
        }
    }
}
