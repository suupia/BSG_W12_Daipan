#nullable enable
using Daipan.Stream.Scripts;
using UnityEngine;


namespace Daipan.Streamer.Scripts
{
    public class StreamViewMono : MonoBehaviour
    {
        [SerializeField] Animator animator = null!;

        readonly IrritatedValue _irritatedValue = null!;

        private void Awake()
        {
            if (animator == null) Debug.LogWarning("animator is null");
        }
        void Update()
        {

        }


    }
}
