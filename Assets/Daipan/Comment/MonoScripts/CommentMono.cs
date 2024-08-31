#nullable enable
using System;
using Daipan.Comment.Scripts;
using Daipan.Core.Interfaces;
using Daipan.LevelDesign.Comment.Scripts;
using TMPro;
using UnityEngine;
using VContainer;

namespace Daipan.Comment.MonoScripts
{
    public sealed class CommentMono : MonoBehaviour
    {
        [SerializeField] TextMeshPro commentText = null!;
        [SerializeField] GameObject commentEffect = null!;
        CommentCluster _commentCluster = null!;
        CommentParamsServer _commentParamsServer = null!;

        string _commentWord = null!;
        float _effectThreshold;
        bool _isEffected = false;

        void Update()
        {
            var direction = (new Vector3(_commentParamsServer.GetDespawnedPosition().x - transform.position.x - commentText.preferredWidth * 0.5f, 0, 0)).normalized;
            transform.position += direction * _commentParamsServer.GetSpeed() * Time.deltaTime;
            if (transform.position.x + commentText.preferredWidth * 0.5f - _commentParamsServer.GetDespawnedPosition().x < 0.001f) _commentCluster.Remove(this);

            // エフェクト生成
            if(transform.position.x - commentText.preferredWidth * 0.5f < _effectThreshold && !_isEffected)
            {
                _isEffected = true;
                Instantiate(commentEffect, new Vector3(_effectThreshold, transform.position.y, 0f), Quaternion.identity);
            }
        }

        public event EventHandler<DespawnEventArgs>? OnDespawn;


        [Inject]
        public void Initialize(
            CommentParamsServer commentParamsServer,   
            CommentCluster commentCluster
        )
        {
            _commentParamsServer = commentParamsServer;
            _commentCluster = commentCluster;

            _effectThreshold = Camera.main.ViewportToWorldPoint(Vector3.one).x;
        }

        public void SetParameter(string commentWord)
        {
            _commentWord = commentWord;
            commentText.text = commentWord;
        }

        public void Despawn()
        {
            var args = new DespawnEventArgs(CommentEnum.None);
            OnDespawn?.Invoke(this, args);
            Destroy(gameObject);
        }

        public void BlownAway()
        {
            _commentCluster.Remove(this);
        }
    }

        public record DespawnEventArgs(CommentEnum CommentEnum);

}