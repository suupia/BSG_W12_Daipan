using Daipan.Stream.Interfaces;
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Stream.MonoScripts
{
    public sealed class StreamMono : MonoBehaviour
    {
        ViewerNumber _viewerNumber = null!;
        ViewerParam _viewerParam = null!;

        float OneSecTimer { get; set; }

        void Update()
        {
            OneSecTimer += Time.deltaTime;
            if (OneSecTimer > 1)
            {
                OneSecTimer = 0;
            }



        }

        [Inject]
        public void Initialize(
            ViewerParam viewerParam,
            ViewerNumber viewerNumber
           ) 
        {
            _viewerParam = viewerParam;
            _viewerNumber = viewerNumber;
        }
    }
}