#nullable enable
using Daipan.Battle.scripts;
using DG.Tweening;
using TMPro;
using UnityEngine;
using R3;
using VContainer;
using UnityEngine.Video;

namespace Daipan.Enemy.MonoScripts
{
    public class WaveTextMono : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI waveText = null!;
        [SerializeField] GameObject videocanvas=null!;
        WaveState _waveState = null!;

        [Inject]
        public void Initialize(WaveState state)
        {
            _waveState = state;
            Observable.EveryValueChanged(state, x => x.CurrentWaveIndex)
                .Subscribe(Show)
                .AddTo(this);
            videocanvas.SetActive(false);
        }

        void Show(int wave)
        {
            waveText.text = wave + 1 != _waveState.TotalWaveCount ? $"Wave {wave + 1}" : "Final Wave";
            if (wave != 0)
            {
                videocanvas.SetActive(true);
            }
            MoveWaveText(transform);
        }


        static void MoveWaveText(Transform transform)
        {
            const float offset = 8; // 画面外から出てくる位置
            const float moveDuration = 1.2f; // AttackとReleaseのそれぞれの時間
            const float showDuration = 0.15f; // holdの時間
            var startPosition = new Vector3(0.0f, -offset, 0.0f);
            var centerPosition = new Vector3(0.0f, 0.0f, 0.0f);
            var endPosition = new Vector3(0.0f, offset, 0.0f);

            DOTween.Sequence()
                .OnStart(() => { transform.position = startPosition; })
                .Append(transform.DOMove(centerPosition, moveDuration / 2).SetEase(Ease.OutQuart))
                .AppendInterval(showDuration)
                .Append(transform.DOMove(endPosition, moveDuration / 2).SetEase(Ease.InQuart))
                .Play();
        }

        public void OnAnimationEnd()
        {
            videocanvas.SetActive(false);
        }
    }
}