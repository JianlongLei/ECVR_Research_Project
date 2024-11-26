using System;

namespace ModularRobot
{
    /// <summary>
    /// Represents a modular robot consisting of a body and a brain.
    /// </summary>
    public class ModularRobot
    {
        private readonly Guid _uuid;

        /// <summary>
        /// Gets the body of the modular robot.
        /// </summary>
        public Body Body { get; }

        /// <summary>
        /// Gets the brain of the modular robot.
        /// </summary>
        public Brain Brain { get; }

        /// <summary>
        /// Gets the UUID of the modular robot, used for identification.
        /// </summary>
        public Guid UUID => _uuid;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModularRobot"/> class.
        /// </summary>
        /// <param name="body">The body of the modular robot.</param>
        /// <param name="brain">The brain of the modular robot.</param>
        public ModularRobot(Body body, Brain brain)
        {
            _uuid = Guid.NewGuid();
            Body = body ?? throw new ArgumentNullException(nameof(body));
            Brain = brain ?? throw new ArgumentNullException(nameof(brain));
        }
    }
}
