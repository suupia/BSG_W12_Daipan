using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Daipan.Stream.Scripts
{
    public class StreamViewMono : MonoBehaviour
    {
        [SerializeField] Image irritatedGauge = null!;
        
        IrritatedValue _irritatedValue = null!;
        
        [Inject]
        public void Initialize(IrritatedValue irritatedValue)
        {
            _irritatedValue = irritatedValue;
        }
        
        void Update()
        {
                irritatedGauge.fillAmount = _irritatedValue.Ratio;
        }
    }
    
}
