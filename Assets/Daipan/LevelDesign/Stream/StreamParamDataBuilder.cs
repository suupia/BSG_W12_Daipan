#nullable enable
using Daipan.Stream.Scripts;
using VContainer;

namespace Daipan.LevelDesign.Stream
{
    public class StreamParamDataBuilder
    {
        public StreamParamDataBuilder(
            IContainerBuilder builder,
            StreamParameter streamParameter
        )
        {
            var streamData = new StreamData()
            {
                GetMaxTime = () => streamParameter.timer.maxTime
            };
            builder.RegisterInstance(streamData);
        }
    }
}