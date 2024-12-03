using System;
using System.Linq;

namespace Revolve2.Utilities
{
    /// <summary>
    /// Utility for extracting generic type arguments from a subclass in C#.
    /// </summary>
    public static class GenericTypeHelper
    {
        /// <summary>
        /// Gets the generic arguments from a child class that inherits from a parent class with generic type parameters.
        /// </summary>
        /// <typeparam name="TChild">The type of the child class.</typeparam>
        /// <typeparam name="TParent">The type of the parent class.</typeparam>
        /// <returns>An array of generic type arguments.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the parent type is not found in the inheritance hierarchy or if it is not generic.
        /// </exception>
        public static Type[] GetGenericArguments<TChild, TParent>()
            where TChild : TParent
        {
            var childType = typeof(TChild);
            var parentType = typeof(TParent);

            // Find the matching parent type in the base types or interfaces
            var genericParent = childType.BaseType;

            while (genericParent != null && genericParent.IsGenericType && genericParent.GetGenericTypeDefinition() != parentType)
            {
                genericParent = genericParent.BaseType;
            }

            if (genericParent == null || !genericParent.IsGenericType)
            {
                throw new InvalidOperationException($"The type {parentType.Name} is not found as a generic base type of {childType.Name}.");
            }

            // Return the generic type arguments
            return genericParent.GetGenericArguments();
        }
    }
}
