using Penguin.Math.Extensions;
using Penguin.Random.Interfaces;

namespace Penguin.Random.Extensions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public static class IRandomGeneratorExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        /// <summary>Selects an ulong beneath the set value</summary>
        /// <param name="rng">The source RNG</param>
        /// <param name="MaxValue">The max value to return (Exclusive)</param>
        /// <returns>The next value in the sequence below the max value</returns>
        public static T Next<T>(this IRandomGenerator<T> rng, T MaxValue)
        {
            if (rng is null)
            {
                throw new System.ArgumentNullException(nameof(rng));
            }

            return rng.Next(default, MaxValue);
        }
    }
}