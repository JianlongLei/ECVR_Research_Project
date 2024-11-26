using System.Numerics;

namespace ModularRobot
{
    public interface IOrientation
    {
        Quaternion Orientation { get; }
    }
}
