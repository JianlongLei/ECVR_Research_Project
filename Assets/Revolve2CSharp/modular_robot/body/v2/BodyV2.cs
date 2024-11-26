namespace ModularRobot
{
    /// <summary>
    /// Represents the body of a V2 modular robot.
    /// </summary>
    public class BodyV2 : Body
    {
        private CoreV2 _core;

        /// <summary>
        /// Initializes a new instance of the <see cref="BodyV2"/> class.
        /// </summary>
        public BodyV2()
            : base(new CoreV2(0.0f))
        {
            _core = (CoreV2)Core;
        }

        /// <summary>
        /// Gets the specific V2 core of the body.
        /// </summary>
        public CoreV2 CoreV2 => _core;
    }
}
