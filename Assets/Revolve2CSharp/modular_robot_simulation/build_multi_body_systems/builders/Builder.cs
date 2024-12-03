using System.Collections.Generic;
using Revolve2.Mapping;
using Revolve2.Simulation;
using Revolve2.Utilities;

namespace Revolve2.Builders
{
    public abstract class Builder
    {
        public abstract List<UnbuiltChild> Build(
            MultiBodySystem multiBodySystem,
            BodyToMultiBodySystemMapping bodyToMultiBodySystemMapping);
    }
}
