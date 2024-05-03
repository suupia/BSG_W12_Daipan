using System.Collections;
using System.Collections.Generic;
using Daipan.Viewer.Scripts;
using UnityEngine;
using VContainer;

public class ViewerMono : MonoBehaviour
{
    ViewerNumberParameter _viewerNumberParameter;
    ViewerNumber _viewerNumber;
    
    float Timer { get; set; }
    [Inject]
    public void Initialize(
        ViewerNumberParameter viewerNumberParameter,
        ViewerNumber viewerNumber)
    {
        _viewerNumberParameter = viewerNumberParameter;
        _viewerNumber = viewerNumber;
    }
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > 1)
        {
            _viewerNumber.IncreaseViewer(_viewerNumberParameter.increaseNumberPerSecond);
            Timer = 0;
        }
    }
}
