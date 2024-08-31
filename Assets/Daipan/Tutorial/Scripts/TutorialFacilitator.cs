#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Daipan.Tutorial.Interfaces;
using UnityEngine;

namespace Daipan.Tutorial.Scripts
{
    public class TutorialCurrentStep
    {
        public ITutorialContent? CurrentStep { get; set; }
    }

    public class TutorialFacilitator : IUpdate
    {
        public ITutorialContent? CurrentStep => _tutorialCurrentStep.CurrentStep;
        readonly Queue<ITutorialContent> _tutorialSteps = new();
        readonly TutorialCurrentStep _tutorialCurrentStep;

        public TutorialFacilitator(IEnumerable<ITutorialContent> tutorialSteps, TutorialCurrentStep tutorialCurrentStep)
        {
            foreach (var step in tutorialSteps)
            {
                _tutorialSteps.Enqueue(step);
            }
            _tutorialCurrentStep = tutorialCurrentStep;
        }

        void IUpdate.Update()
        {
            RunTutorial();
        }

        void RunTutorial()
        {
            if (_tutorialCurrentStep.CurrentStep == null || _tutorialCurrentStep.CurrentStep.IsCompleted())
            {
                _tutorialCurrentStep.CurrentStep = _tutorialSteps.Dequeue();
                _tutorialCurrentStep.CurrentStep?.Execute();
            }

            if (_tutorialSteps.Count == 0 && _tutorialCurrentStep.CurrentStep?.IsCompleted() == true)
                Debug.Log("Tutorial completed!");
        }
    }
}