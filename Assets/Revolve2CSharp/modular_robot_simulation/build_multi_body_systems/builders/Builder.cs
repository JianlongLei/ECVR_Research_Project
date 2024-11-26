using System.Collections.Generic;
using ModularRobot.Mapping;
using ModularRobot.Simulation;
using ModularRobot.Utilities;

namespace ModularRobot.Builders
{
    public abstract class Builder
    {
        public abstract List<UnbuiltChild> Build(
            MultiBodySystem multiBodySystem,
            BodyToMultiBodySystemMapping bodyToMultiBodySystemMapping);
    }
}
