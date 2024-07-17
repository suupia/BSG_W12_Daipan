#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.Option.Scripts;
using VContainer;
using R3;

namespace Daipan.Option.MonoScripts
{
    public class OptionPopUpViewMono : MonoBehaviour
    {
        [SerializeField] GameObject mainOptionCanvas = null!;

        [Inject]
        public void Initialize(OptionController optionController)
        {
            Observable.EveryValueChanged(optionController, x => x.IsOpening).
                Subscribe(x =>
                {
                    if (optionController.IsOpening)
                    {
                        OpenPopUp();
                    }
                    else
                    {
                        ClosePopUp();
                    }
                }).
                AddTo(this);
        }

        void OpenPopUp()
        {
            mainOptionCanvas?.SetActive(true);
        }
        public void ClosePopUp()
        {
            mainOptionCanvas?.SetActive(false);
        }
    }   
}