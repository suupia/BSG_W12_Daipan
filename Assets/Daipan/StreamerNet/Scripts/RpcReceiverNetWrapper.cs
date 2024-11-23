#nullable enable 
using Daipan.StreamerNet.MonoScripts;
using UnityEngine;

namespace Daipna.StreamerNet.Scripts
{
    public class RpcReceiverNetWrapper
    {
        RpcReceiverNet? _rpcReceiverNet;
        public RpcReceiverNet RpcReceiverNet => _rpcReceiverNet ??= Object.FindObjectOfType<RpcReceiverNet>();
    }
}