#nullable enable

using System;
using Cysharp.Threading.Tasks;
using Daipan.Core;
using Daipan.Daipan;
using Fusion;
using UnityEngine;

public class DaipanInitializerMono : MonoBehaviour
{
    async void Awake()
    {
        var runner = FindObjectOfType<NetworkRunner>();
        Debug.Log($"NetworkRunner : {runner}");

        await UniTask.WaitUntil(() => runner.IsCloudReady); 
        
        var dtoNet = FindObjectOfType<DTONet>(); 
        // これは存在していることを確認するためだけのログ。
        // VContainerのBuildの中でも再度FindObjectする。
        Debug.Log($"DTONet : {dtoNet}"); 

        
        var daipanScopeNet = FindObjectOfType<DaipanScopeNet>();
        Debug.Log($"DaipanScopeNet : {daipanScopeNet}");
        
        await UniTask.Run(() => daipanScopeNet.Build());
    }
}