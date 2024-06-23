#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Daipan.Stream.Scripts;
using R3;
using UnityEngine;

namespace Daipan.Battle.scripts
{
    public class EndSceneSelector : IStart, IDisposable
    {
        readonly ViewerNumber _viewerNumber;
        
        readonly IList<IDisposable> _disposables = new List<IDisposable>(); 
        
        public EndSceneSelector(ViewerNumber viewerNumber)
        {
            _viewerNumber = viewerNumber;
        }

        void IStart.Start()
        {
            SetUp();
        }

        void SetUp()
        {
            _disposables.Add( Observable.EveryUpdate()
                .Subscribe(_ => ChangeToInsideTheBox(_viewerNumber))   );
        }

        static void ChangeToInsideTheBox(ViewerNumber viewerNumber)
        {
            Debug.Log("Check ChangeToInsideTheBox");
            if(viewerNumber.Number >= 500)  // todo : receive parameter from inspector
            {
                Debug.Log("Change to InsideTheBox");
                SceneTransition.TransitioningScene(SceneName.InsideTheBox);
            }
        }
        
        
        
        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
        
        ~EndSceneSelector()
        {
            Dispose();
        }

    }
}