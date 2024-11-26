namespace ModularRobot
{
    /// <summary>
    /// A brain that does nothing.
    /// </summary>
    public class BrainDummy : Brain
    {
        /// <summary>
        /// Creates an instance of this brain.
        /// </summary>
        /// <returns>The created brain instance.</returns>
        public override BrainInstance MakeInstance()
        {
            return new BrainDummyInstance();
        }
    }
}
