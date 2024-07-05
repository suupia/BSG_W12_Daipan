#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.Comment.MonoScripts;

public class CommentTesterMono : MonoBehaviour
{
    [SerializeField] CommentParamManager commentParamManager = null!;
    [SerializeField] CommentMono commentMono = null!;

//#if UNITY_EDITOR
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            for(int i = 0; i < commentParamManager.commentWords.Count; i ++)
            {
                var c = Instantiate(commentMono);
                c.SetParameter(commentParamManager.commentWords[i]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            for (int i = 0; i < commentParamManager.antiCommentWords.Count; i++)
            {
                var c = Instantiate(commentMono);
                c.SetParameter(commentParamManager.antiCommentWords[i]);
            }
        }
    }
//#endif
}
