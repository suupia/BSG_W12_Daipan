#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Stream.Scripts;
using R3;
using UnityEngine;

namespace Daipan.Battle.scripts
{
    public class EndSceneSelector : IDisposable
    {
        readonly ViewerNumber _viewerNumber;
        
        readonly IList<IDisposable> _disposables = new List<IDisposable>(); 
        
        public EndSceneSelector(ViewerNumber viewerNumber)
        {
            _viewerNumber = viewerNumber;
        }

        public void SetUp()
        {
            _disposables.Add( Observable.EveryUpdate()
                .Subscribe(_ => ChangeToInsideTheBox(_viewerNumber))   );
        }

        static void ChangeToInsideTheBox(ViewerNumber viewerNumber)
        {
            if(viewerNumber.Number >= 10)
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