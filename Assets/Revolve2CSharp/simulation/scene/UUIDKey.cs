using System;

namespace ModularRobot.Utilities
{
    /// <summary>
    /// Represents a unique key for objects using their UUID.
    /// </summary>
    public class UUIDKey<T>
    {
        private readonly Guid _uuid;

        public UUIDKey(T obj)
        {
            if (obj is IUUIDProvider provider)
            {
                _uuid = provider.UUID;
            }
            else
            {
                throw new ArgumentException("Object must implement IUUIDProvider.");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is not UUIDKey<T> other) return false;
            return _uuid.Equals(other._uuid);
        }

        public override int GetHashCode()
        {
            return _uuid.GetHashCode();
        }
    }

    /// <summary>
    /// Interface to provide UUIDs for objects.
    /// </summary>
    public interface IUUIDProvider
    {
        Guid UUID { get; }
    }
}
