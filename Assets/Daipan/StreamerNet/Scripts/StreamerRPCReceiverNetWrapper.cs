#nullable enable 
using Daipan.StreamerNet.MonoScripts;
using UnityEngine;

namespace Daipna.StreamerNet.Scripts
{
    public class StreamerRPCReceiverNetWrapper
    {
        StreamerRPCReceiverNet? _streamerRPCReceiverNet;
        public StreamerRPCReceiverNet RPCReceiverNet => _streamerRPCReceiverNet ??= Object.FindObjectOfType<StreamerRPCReceiverNet>();
    }
}