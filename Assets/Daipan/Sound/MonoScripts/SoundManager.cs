#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using R3;
using UnityEngine;
using UnityEngine.Audio;

namespace Daipan.Sound.MonoScripts
{
    public sealed class SoundManager : MonoBehaviour, IDisposable
    {
        [SerializeField] List<BgmParam> bgmParams = null!;
        [SerializeField] List<SeParam> seParams = null!;

        // by GPT o1
        // --- ここから追加項目 ---
        [Header("SE再生制御用パラメータ")]
        [SerializeField, Range(1, 10)] private int delayFrameCount = 2;
        [SerializeField, Range(1, 32)] private int maxQueuedItemCount = 4;
        [SerializeField, Range(1, 8)] private int concurrencyLimit = 1; 
        // concurrencyLimit: 同一SEを同時(1フレーム以内)に再生できる数。
        // 1にすれば1フレームに1回のみ即時再生。それ以上はキューに回す。

        // キューに格納するSE再生要求の内部クラス
        private class SeRequest
        {
            public SeEnum seEnum;
            public AudioClip clip;
            public float volumeMultiplier;
            public int frameCount;
        }

        // SEごとにキューを管理する辞書
        private Dictionary<SeEnum, Queue<SeRequest>> seQueues = new Dictionary<SeEnum, Queue<SeRequest>>();
        // 現在フレームに再生した同一SEの数をカウント
        private Dictionary<SeEnum, int> sePlayCountPerFrame = new Dictionary<SeEnum, int>();
        // --- ここまで追加項目 ---

        static SoundManager? _instance;
        public static SoundManager? Instance => _instance;

        public static float BgmVolume
        {
            set => _bgmVolume = Mathf.Clamp(value / 7f, 0, 1);
            get => (int)(_bgmVolume * 7);
        }

        static float _bgmVolume;

        public static float SeVolume
        {
            set => _seVolume = Mathf.Clamp(value / 7f, 0, 1);
            get => (int)(_seVolume * 7);
        }

        static float _seVolume;

        readonly CompositeDisposable _disposable = new();

        public void Initialize()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                Debug.Log("SoundManager is created");
            }
            else
            {
                Debug.Log("SoundManager is already created");
            }

            foreach (var bgmParam in bgmParams) bgmParam.audioSource.clip = bgmParam.audioClip;
            foreach (var seParam in seParams) seParam.audioSource.clip = seParam.audioClip;
            BgmVolume = 3;
            SeVolume = 4;

            _disposable.Add(Observable.EveryUpdate().Subscribe(_ =>
            {
                foreach (var bgmParam in bgmParams) bgmParam.audioSource.volume = _bgmVolume;
                foreach (var seParam in seParams) seParam.audioSource.volume = _seVolume * seParam.volumeMultiplier;

                // --- ここから追加項目（UpdateでSEキューを処理） ---
                ProcessSeQueues();
                // --- ここまで追加項目 ---
            }));
        }

        public void PlayBgm(BgmEnum bgmEnum)
        {
            var bgmParam = bgmParams.Find(x => x.bgmEnum == bgmEnum);
            if (bgmParam == null)
            {
                Debug.LogError($"Not found BGM: {bgmEnum}");
                return;
            }

            const float fadeSec = 1f;

            // Stop other BGMs with fade-out
            foreach (var param in bgmParams)
            {
                if (param.bgmEnum != bgmEnum && param.audioSource.isPlaying)
                    param.audioSource.DOFade(0, fadeSec).OnComplete(() => param.audioSource.Stop());
            }

            bgmParam.audioSource.volume = 0;
            bgmParam.audioSource.Play();
            bgmParam.audioSource.DOFade(_bgmVolume, fadeSec);
        }

        public void PlaySe(SeEnum seEnum)
        {
            var seParam = seParams.Find(x => x.seEnum == seEnum);
            if (seParam == null)
            {
                Debug.LogError($"Not found SE: {seEnum}");
                return;
            }

            // このフレーム内でこのSEを再生した回数を取得
            if (!sePlayCountPerFrame.TryGetValue(seEnum, out var currentCount))
            {
                currentCount = 0;
            }

            // concurrencyLimitを超えたらキューへ遅延再生要求を入れる
            if (currentCount >= concurrencyLimit)
            {
                // キューが存在しない場合は作成
                if (!seQueues.ContainsKey(seEnum))
                {
                    seQueues[seEnum] = new Queue<SeRequest>();
                }

                // キューが最大登録数を超えていたら破棄
                if (seQueues[seEnum].Count >= maxQueuedItemCount)
                {
                    Debug.Log($"[SE] Queue overflow. Drop SE request: {seEnum}");
                    return;
                }

                // キューに追加
                var request = new SeRequest
                {
                    seEnum = seEnum,
                    clip = seParam.audioClip,
                    volumeMultiplier = seParam.volumeMultiplier,
                    frameCount = 0
                };
                seQueues[seEnum].Enqueue(request);
            }
            else
            {
                // 即時再生
                seParam.audioSource.volume = _seVolume * seParam.volumeMultiplier;
                seParam.audioSource.PlayOneShot(seParam.audioClip);
                sePlayCountPerFrame[seEnum] = currentCount + 1;
                // Debug.Log($"Play SE: {seEnum}, volume: {seParam.audioSource.volume}");
            }
        }

        private void ProcessSeQueues()
        {
            // 毎フレーム開始時、当フレーム内の再生カウントをリセット
            // （この処理をすることで「同じフレーム内に複数回呼ばれたら」という判定ができる）
            // 本来はOnBeginFrame的なものがあればそこでやりたいが、簡易的に毎フレームの先頭でクリア
            foreach (var key in sePlayCountPerFrame.Keys.ToList())
            {
                sePlayCountPerFrame[key] = 0;
            }

            // キューを処理
            // 各SEEnumに対応するキューを取り出し、先頭からframeCountをインクリメント
            // frameCount > delayFrameCountになったら再生する。
            foreach (var kvp in seQueues.ToList())
            {
                var seEnum = kvp.Key;
                var queue = kvp.Value;

                if (queue.Count == 0) continue;

                int initialCount = queue.Count;
                int playedCount = 0; 
                
                // 同じSEで複数再生可能なフレーム内最大数(concurrencyLimit)を考慮
                // このフレーム内で何回再生済みかを取得
                if (!sePlayCountPerFrame.TryGetValue(seEnum, out var currentCount))
                {
                    currentCount = 0;
                }

                // 再生が可能な回数を算出
                int canPlayCount = Math.Max(0, concurrencyLimit - currentCount);

                // キューを一時的に格納するためのリストを作成（再生判定用）
                var tempList = queue.ToArray();
                var newQueue = new Queue<SeRequest>();

                foreach (var req in tempList)
                {
                    req.frameCount++;
                    if (req.frameCount > delayFrameCount && canPlayCount > 0)
                    {
                        // 再生可能
                        var seParam = seParams.Find(x => x.seEnum == req.seEnum);
                        if (seParam != null)
                        {
                            seParam.audioSource.volume = _seVolume * req.volumeMultiplier;
                            seParam.audioSource.PlayOneShot(req.clip);
                            playedCount++;
                            canPlayCount--;
                            sePlayCountPerFrame[seEnum] = sePlayCountPerFrame.TryGetValue(seEnum, out var countVal) ? countVal + 1 : 1;
                        }
                        else
                        {
                            Debug.LogError($"SE param not found for {req.seEnum}");
                        }
                    }
                    else
                    {
                        // まだ再生出来ない、あるいは再生枠がないのでキューに戻す
                        // frameCountがdelayFrameCount未満なら待機中
                        if (req.frameCount <= delayFrameCount || canPlayCount == 0)
                        {
                            newQueue.Enqueue(req);
                        }
                        else
                        {
                            // delayFrameCount超えているのにcanPlayCount=0の場合、このフレームは再生せず持ち越し
                            newQueue.Enqueue(req);
                        }
                    }
                }

                // 更新されたキューを戻す
                seQueues[seEnum] = newQueue;
            }

            // 空になったSE列挙は削除
            var keysToRemove = seQueues.Where(kvp => kvp.Value.Count == 0).Select(kvp => kvp.Key).ToList();
            foreach (var key in keysToRemove)
            {
                seQueues.Remove(key);
            }
        }

        public void FadOutBgm(float fadeSec)
        {
            foreach (var param in bgmParams)
            {
                if (param.audioSource.isPlaying)
                {
                    param.audioSource.DOFade(0, fadeSec).OnComplete(() => param.audioSource.Stop());
                }
            }
        }

        public void StopAllBgm()
        {
            foreach (var param in bgmParams)
                if (param.audioSource.isPlaying)
                    param.audioSource.Stop();
        }
        
        public void StopAllSe()
        {
            foreach (var param in seParams)
                if (param.audioSource.isPlaying)
                    param.audioSource.Stop();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        ~SoundManager()
        {
            Dispose();
        }
    }

    [Serializable]
    public sealed class BgmParam
    {
        public BgmEnum bgmEnum;
        public AudioSource audioSource = null!;
        public AudioClip audioClip = null!;
    }

    [Serializable]
    public sealed class SeParam
    {
        public SeEnum seEnum;
        public AudioSource audioSource = null!;
        public AudioClip audioClip = null!;
        [Range(0f, 2f)] public float volumeMultiplier = 1;
    }

    public enum BgmEnum
    {
        Title,
        Tutorial,
        Daipan,
        EndScene
    }

    public enum SeEnum
    {
        SpawnComment,
        SpawnAntiComment,
        AttackDeflect,
        Attack,
        Daipan,

        // EndScene
        Hakononaka, 
        Kansyasai, 
        NoobGamer, 
        ProGamer, 
        Seijo, 
        Enjou, 
        Genkai, 
        Heibon,

        Decide,
        Text,
        TowerDamage,
        BAN,
        SelectLanguage,
        Cancel,

        EnemyDieContraction,
        EnemyDieExplosion,
        DaipanWord,
        DaipanInput,
        FeverTime,
        CountUpViewer,
        CountDownViewer,
    }
}
