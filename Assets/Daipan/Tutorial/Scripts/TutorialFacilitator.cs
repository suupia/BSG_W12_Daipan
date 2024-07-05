#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Tutorial.Interfaces;
using UnityEngine;

namespace Daipan.Tutorial.Scripts
{
    public class TutorialFacilitator
    {
        readonly Queue<ITutorialContent> _tutorialSteps = new();

        public TutorialFacilitator()
        {
            InitializeTutorialSteps();
        }

        void InitializeTutorialSteps()
        {
            _tutorialSteps.Enqueue(new DisplayBlackScreenWithProgress());
            _tutorialSteps.Enqueue(new LanguageSelection());
            _tutorialSteps.Enqueue(new FadeInTutorialStart());
            _tutorialSteps.Enqueue(new CatSpeaks());
            _tutorialSteps.Enqueue(new RedEnemyTutorial());
            _tutorialSteps.Enqueue(new SequentialEnemyTutorial());
            _tutorialSteps.Enqueue(new ShowWhiteComments());
            _tutorialSteps.Enqueue(new ShowAntiComments());
            _tutorialSteps.Enqueue(new DaipanCutscene());
            _tutorialSteps.Enqueue(new CatSpeaksAfterDaipan());
            _tutorialSteps.Enqueue(new AimForTopStreamer());
            _tutorialSteps.Enqueue(new StartActualGame());
        }

        public void RunTutorial()
        {
            while (_tutorialSteps.Count > 0)
            {
                ITutorialContent currentStep = _tutorialSteps.Dequeue();
                currentStep.Execute();

                while (!currentStep.IsCompleted())
                {
                    // Wait or do something until the step is completed
                    // Here we simply break the loop assuming each step completes immediately
                    break;
                }
            }

            Debug.Log("Tutorial completed!");
        } 
    } 
}
