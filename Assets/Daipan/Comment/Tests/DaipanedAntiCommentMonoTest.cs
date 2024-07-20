#nullable enable
using Daipan.Comment.MonoScripts;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Daipan.Comment.Tests
{
    public class DaipanedAntiCommentMonoTest : MonoBehaviour
    {
        [SerializeField]  TextMeshPro commentText = null!;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log($"Call Daipaned");
                DaipanedSequence();
            }
        }
        void DaipanedSequence()
        {
            var sequence = DOTween.Sequence()
                .Append(commentText.transform.DOScaleY(0.3f, 0.2f).SetEase(Ease.InQuint)) // 縮めて、
                .Join(commentText.transform.DOMoveY(-0.6f, 0.2f).SetEase(Ease.InQuint) )  // 同時に下に移動
                .Append(commentText.transform.DOScaleY(1.1f, 0.15f).SetEase(Ease.InCubic)) // 素早く大きくする
                .Join(commentText.transform.DOMoveY(0.6f, 0.15f).SetEase(Ease.InCubic) )  // 同時に上に移動
                .Append(commentText.transform.DOScaleY(0, 0.4f).SetEase(Ease.InCubic)) // 小さくしながら
                .Join(commentText.transform.DOMoveY(-1, 0.4f).SetEase(Ease.InCubic) )  // 同時に下に移動
                .OnComplete(() =>
                {
                    // Destroy(gameObject);
                });
            sequence.Play();
        }
    }
}