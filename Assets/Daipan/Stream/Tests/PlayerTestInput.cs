#nullable enable
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Stream.Tests
{
    public sealed class PlayerTestInput : MonoBehaviour
    {
        [SerializeField] CustomButton isExcitedButton = null!;
        [SerializeField] CustomButton isIrritatedButton = null!;
        [SerializeField] CustomButton daiPanButton = null!;
        DaipanExecutor _daipanExecutor = null!;

        void Awake()
        {
      
            daiPanButton.OnClick += () => { _daipanExecutor.DaiPan(); };
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("DaiPan!");
                _daipanExecutor.DaiPan();
            }
        }


        [Inject]
        public void Construct(
            DaipanExecutor daipanExecutor
  )
        {
            _daipanExecutor = daipanExecutor;
        }
    }
}