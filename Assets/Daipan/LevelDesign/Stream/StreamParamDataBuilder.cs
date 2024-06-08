#nullable enable
using Daipan.Stream.Scripts;
using VContainer;

namespace Daipan.LevelDesign.Stream
{
    public class StreamParamDataBuilder
    {
        public StreamParamDataBuilder(
            IContainerBuilder builder,
            StreamParam streamParam
        )
        {
            var streamData = new StreamData()
            {
                //GetMaxTime = () => streamParam.timer.maxTime
            };
            builder.RegisterInstance(streamData);
        }
    }
}