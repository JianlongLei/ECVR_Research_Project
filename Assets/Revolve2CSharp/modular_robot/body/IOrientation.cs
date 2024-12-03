using System.Numerics;

namespace Revolve2.Robot
{
    public interface IOrientation
    {
        Quaternion Orientation { get; }
    }
}
