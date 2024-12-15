#nullable enable
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour,
    IPointerClickHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    public event Action? onPointerDown; 
    public event Action? onPointerUp; 
    public event Action? onClick;
    public bool IsInteractable { get; set; } = true;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!IsInteractable) return;
        onPointerDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!IsInteractable) return;
        onPointerUp?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsInteractable) return;
        onClick?.Invoke();
    }
    
  
}