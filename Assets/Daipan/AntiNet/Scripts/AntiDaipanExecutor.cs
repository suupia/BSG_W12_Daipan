#nullable enable

using Daipan.Comment.Scripts;
using Daipan.Sound.MonoScripts;
using Daipan.Stream.MonoScripts;
using Daipan.Streamer.MonoScripts;
using VContainer;

namespace Daipan.AntiNet.Scripts
{
    public class AntiDaipanExecutor
    {
        readonly AntiCommentCluster _antiCommentCluster;
        readonly StreamerViewMono _streamerViewMono;
        readonly ShakeDisplayMono _shakeDisplayMono;
        readonly AntiStateValue _antiStateValue;
        public int DaipanCount { get; private set; }
        [Inject]
        public AntiDaipanExecutor(
            AntiCommentCluster antiCommentCluster,
            StreamerViewMono streamerViewMono,
            ShakeDisplayMono shakeDisplayMono,
            AntiStateValue antiStateValue
        )
        {
            _antiCommentCluster = antiCommentCluster;
            _streamerViewMono = streamerViewMono;
            _shakeDisplayMono = shakeDisplayMono;
            _antiStateValue = antiStateValue;
        }
        public void DaiPan()
        {
            _antiCommentCluster.Daipaned();
            _streamerViewMono.Daipan();
            _shakeDisplayMono.Daipan();
            DaipanCount++;
            _antiStateValue.SetBan(true);
            SoundManager.Instance?.PlaySe(SeEnum.Daipan);
        }
    }
}