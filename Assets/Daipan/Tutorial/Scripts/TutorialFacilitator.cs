#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Daipan.Tutorial.Interfaces;
using UnityEngine;

namespace Daipan.Tutorial.Scripts
{
    public class TutorialFacilitator : IUpdate
    {
        public ITutorialContent? CurrentStep => _currentStep;
        readonly Queue<ITutorialContent> _tutorialSteps = new();
        ITutorialContent? _currentStep;

        public TutorialFacilitator(IEnumerable<ITutorialContent> tutorialSteps)
        {
            foreach (var step in tutorialSteps)
            {
                _tutorialSteps.Enqueue(step);
            }
        }

        void IUpdate.Update()
        {
            RunTutorial();
        }

        void RunTutorial()
        {
            if (_currentStep == null || _currentStep.IsCompleted())
            {
                _currentStep = _tutorialSteps.Dequeue();
                _currentStep?.Execute();
            }

            if (_tutorialSteps.Count == 0 && _currentStep?.IsCompleted() == true)
                Debug.Log("Tutorial completed!");
        }
    }
}