#nullable enable
using Daipan.Tutorial.Interfaces;
using UnityEngine;

namespace Daipan.Tutorial.Scripts
{
    internal class DisplayBlackScreenWithProgress : ITutorialContent
    {
        bool _completed = false;

        public void Execute()
        {
            Debug.Log("Displaying black screen with download progress...");
            // Logic for this step
            _completed = true; // Set to true once the step is done
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class LanguageSelection : ITutorialContent
    {
        bool _completed = false;

        public void Execute()
        {
            Debug.Log("Language selection...");
            // Logic for this step
            _completed = true; // Set to true once the step is done
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class FadeInTutorialStart : ITutorialContent
    {
        bool _completed = false;
        CanvasGroup _canvasGroup;
        float _fadeDuration = 1.0f; // フェードインの時間を秒単位で指定
        float _fadeTime = 0f;

        public FadeInTutorialStart()
        {
            _canvasGroup.alpha = 0f; // 初期状態で透明
        }

        public void Execute()
        {
            Debug.Log("Starting tutorial with fade-in effect...");
            _fadeTime += Time.deltaTime;

            if (_fadeTime < _fadeDuration)
            {
                _canvasGroup.alpha = _fadeTime / _fadeDuration;
            }
            else
            {
                _canvasGroup.alpha = 1f;
                _completed = true;
            }
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class CatSpeaks : ITutorialContent
    {
        bool _completed = false;

        public void Execute()
        {
            Debug.Log("Streamer wakes up...");
            Debug.Log("Cat speaks...");
            // Logic for this step
            _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class RedEnemyTutorial : ITutorialContent
    {
        bool _completed = false;

        public void Execute()
        {
            Debug.Log("Tutorial: Defeat the red enemy...");
            // 敵を倒せれば上手！！
            // そうでなかったら、もう一回！
            // Logic for this step
            _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class SequentialEnemyTutorial : ITutorialContent
    {
        bool _completed = false;

        public void Execute()
        {
            Debug.Log("Tutorial: Defeat enemies in sequence...");
            // Logic for this step
            _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class ShowWhiteComments : ITutorialContent
    {
        bool _completed = false;

        public void Execute()
        {
            Debug.Log("Displaying white comments...");
            // Logic for this step
            _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class ShowAntiComments : ITutorialContent
    {
        bool _completed = false;

        public void Execute()
        {
            Debug.Log("Showing anti-comments with sound effects...");
            Debug.Log("Anger gauge animation...");
            // Logic for this step
            _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }


    internal class DaipanCutscene : ITutorialContent
    {
        bool _completed = false;

        public void Execute()
        {
            Debug.Log("Displaying special cutscene...");
            // 特別なカットに切り替える
            // Logic for this step
            _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }


    internal class CatSpeaksAfterDaipan : ITutorialContent
    {
        bool _completed = false;

        public void Execute()
        {
            Debug.Log("Cat speaks more...");
            // Logic for this step
            _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class AimForTopStreamer : ITutorialContent
    {
        bool _completed = false;

        public void Execute()
        {
            Debug.Log("Aim for top streamer...");
            // Logic for this step
            _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }

    internal class StartActualGame : ITutorialContent
    {
        bool _completed = false;

        public void Execute()
        {
            Debug.Log("Starting actual game...");
            // Logic for this step
            _completed = true;
        }

        public bool IsCompleted()
        {
            return _completed;
        }
    }
}