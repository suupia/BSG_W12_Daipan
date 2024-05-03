using System.Collections;
using System.Collections.Generic;
using Daipan.Viewer.Scripts;
using UnityEngine;
using VContainer;

public class ViewerMono : MonoBehaviour
{
    ViewerNumberParameter _viewerNumberParameter;
    ViewerNumber _viewerNumber;
    DistributionStatus _distributionStatus;
    
    float Timer { get; set; }
    [Inject]
    public void Initialize(
        ViewerNumberParameter viewerNumberParameter,
        ViewerNumber viewerNumber,
        DistributionStatus distributionStatus)
    {
        _viewerNumberParameter = viewerNumberParameter;
        _viewerNumber = viewerNumber;
        _distributionStatus = distributionStatus;
    }
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > 1)
        {
            if(_distributionStatus.ExistIrrationalFactors)
                _viewerNumber.DecreaseViewer(_viewerNumberParameter.decreaseNumberWhenIrradiated);
            else
                _viewerNumber.IncreaseViewer(_viewerNumberParameter.increaseNumberPerSecond);
            Timer = 0;
        }
    }
}
