#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class DaipanScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
       // Domain
       // Stream
       builder.Register<IrritatedValue>(Lifetime.Scoped).WithParameter(100);
       
       // View
       builder.RegisterComponentInHierarchy<StreamViewMono>();
    }
    
}
