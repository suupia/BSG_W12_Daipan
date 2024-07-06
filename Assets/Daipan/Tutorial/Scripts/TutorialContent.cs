#nullable enable
using Daipan.Tutorial.Interfaces;
using Daipan.Tutorial.MonoScripts;
using UnityEngine;

namespace Daipan.Tutorial.Scripts
{
    internal class DisplayBlackScreenWithProgress : ITutorialContent
    {
        bool _completed;
        readonly DownloadGaugeViewMono _gaugeViewMono;
        double _fillAmount;
        const double FillAmountPerSec = 0.2;

        public DisplayBlackScreenWithProgress(DownloadGaugeViewMono gaugeViewMono)
        {
            _gaugeViewMono = gaugeViewMono;
        }

        public void ExecuteUpdate()
        {
            Debug.Log("Displaying black screen with download progress...");
            _fillAmount += FillAmountPerSec * Time.deltaTime;
            _gaugeViewMono.SetGaugeValue((float)_fillAmount);
            if (_fillAmount >= 0.5) _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class LanguageSelection : ITutorialContent
    {
        bool _completed = false;

        public void ExecuteUpdate()
        {
            Debug.Log("Language selection...");
            // Logic for this step
            if (Input.GetKeyDown(KeyCode.T)) _completed = true; // Set to true once the step is done
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class FadeInTutorialStart : ITutorialContent
    {
        bool _completed = false;


        public void ExecuteUpdate()
        {
            if (Input.GetKeyDown(KeyCode.T)) _completed = true; // Set to true once the step is done
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class CatSpeaks : ITutorialContent
    {
        bool _completed = false;

        public void ExecuteUpdate()
        {
            Debug.Log("Streamer wakes up...");
            Debug.Log("Cat speaks...");
            // Logic for this step
            if (Input.GetKeyDown(KeyCode.T)) _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class RedEnemyTutorial : ITutorialContent
    {
        bool _completed = false;

        public void ExecuteUpdate()
        {
            Debug.Log("Tutorial: Defeat the red enemy...");
            // 敵を倒せれば上手！！
            // そうでなかったら、もう一回！
            // Logic for this step
            if (Input.GetKeyDown(KeyCode.T)) _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class SequentialEnemyTutorial : ITutorialContent
    {
        bool _completed = false;

        public void ExecuteUpdate()
        {
            Debug.Log("Tutorial: Defeat enemies in sequence...");
            // Logic for this step
            if (Input.GetKeyDown(KeyCode.T)) _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class ShowWhiteComments : ITutorialContent
    {
        bool _completed = false;

        public void ExecuteUpdate()
        {
            Debug.Log("Displaying white comments...");
            // Logic for this step
            if (Input.GetKeyDown(KeyCode.T)) _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class ShowAntiComments : ITutorialContent
    {
        bool _completed = false;

        public void ExecuteUpdate()
        {
            Debug.Log("Showing anti-comments with sound effects...");
            Debug.Log("Anger gauge animation...");
            // Logic for this step
            if (Input.GetKeyDown(KeyCode.T)) _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }


    internal class DaipanCutscene : ITutorialContent
    {
        bool _completed = false;

        public void ExecuteUpdate()
        {
            Debug.Log("Displaying special cutscene...");
            // 特別なカットに切り替える
            // Logic for this step
            if (Input.GetKeyDown(KeyCode.T)) _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }


    internal class CatSpeaksAfterDaipan : ITutorialContent
    {
        bool _completed = false;

        public void ExecuteUpdate()
        {
            Debug.Log("Cat speaks more...");
            // Logic for this step
            if (Input.GetKeyDown(KeyCode.T)) _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class AimForTopStreamer : ITutorialContent
    {
        bool _completed = false;

        public void ExecuteUpdate()
        {
            Debug.Log("Aim for top streamer...");
            // Logic for this step
            if (Input.GetKeyDown(KeyCode.T)) _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class StartActualGame : ITutorialContent
    {
        bool _completed = false;

        public void ExecuteUpdate()
        {
            Debug.Log("Starting actual game...");
            // Logic for this step
            if (Input.GetKeyDown(KeyCode.T)) _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }
}