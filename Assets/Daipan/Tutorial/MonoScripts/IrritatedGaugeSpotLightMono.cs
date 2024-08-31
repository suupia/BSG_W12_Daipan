using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class IrritatedGaugeSpotLightMono : MonoBehaviour
{
    [SerializeField] GameObject shadowObject = null;
    [SerializeField] RectTransform unmaskRect = null;
    Sequence _sequence; // Hold the sequence to manage animations
    
    const float MaxScale = 20; // Maximum scale value for the unmask rect
    void Awake()
    {
        if (shadowObject == null || unmaskRect == null)
        {
            Debug.LogError("Assign all required fields in the inspector.");
            enabled = false; // Disable the script if setup is incomplete
            return;
        }
        
        shadowObject.SetActive(false);
        unmaskRect.localScale = Vector3.one * MaxScale; // Set initial large scale
    }

    public void Show()
    {
        shadowObject.SetActive(true);
        unmaskRect.localScale = Vector3.one * MaxScale; // Reset scale to initial large value before animation
        _sequence .Append(unmaskRect.DOScale(Vector3.one * 2, 1f)); // Animate scale from current to 2 over 3 seconds
    }
    
    public void Hide()
    {
        if (_sequence.IsActive()) 
        {
            _sequence.Kill(); // Kill the current animation if active
        }
        unmaskRect.DOScale(Vector3.one * MaxScale, 0.8f).OnComplete(() => {
            shadowObject.SetActive(false);
        }); // Animate scale and disable shadow object on completion
    }
}