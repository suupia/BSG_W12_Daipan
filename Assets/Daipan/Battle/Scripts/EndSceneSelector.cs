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
        
        public EndSceneSelector(
            ViewerNumber viewerNumber
            )
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
                .Subscribe(_ =>
                {
                    ChangeToInsideTheBox(_viewerNumber);
                    ChangeToThanksgiving(_viewerNumber);
                })   );
        }

        static void ChangeToInsideTheBox(ViewerNumber viewerNumber)
        {
            Debug.Log("Check ChangeToInsideTheBox");
            if(viewerNumber.Number >= 500)  // todo : receive parameter from inspector
            {
                Debug.Log("Change to InsideTheBox");
                ResultShower.ShowResult(SceneName.InsideTheBox);
            }
        }
       
        static void ChangeToThanksgiving(ViewerNumber viewerNumber)
        {
            Debug.Log("Check ChangeToThanksgiving");
            if(viewerNumber.Number >= 1000)  // todo : receive parameter from inspector
            {
                Debug.Log("Change to Thanksgiving");
                ResultShower.ShowResult(SceneName.Thanksgiving);
            }
        }
        
        // todo: 対応する遷移の処理を追加する
        // その時必要な依存関係は注入するようにする。
        
        
        
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