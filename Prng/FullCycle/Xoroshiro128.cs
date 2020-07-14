﻿// Decompiled with JetBrains decompiler
// Type: Penguin.Random.Prng.FullCycle.Xoroshiro128
// Assembly: Penguin.Random, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A5518E0-DEF6-4913-9205-059E59B1CA91
// Assembly location: C:\Git\MassageEnvy.com Umbraco 8\MassageEnvy\bin\Penguin.Random.dll

using Penguin.Random.Interfaces;
using System;
using System.Runtime.CompilerServices;

namespace Penguin.Random.Prng.FullCycle
{
    /// <summary>And implementation of a Xoroshiro128 RNG</summary>
    public class Xoroshiro128 : IRandomGenerator<ulong>
    {
        private static ulong[] JumpValue = new ulong[2]
        {
      13739361407582206667UL,
      15594563132006766882UL
        };

        private ulong _seed1;
        private ulong _seed2;

        /// <summary>
        /// Creates a new instance of Xoroshiro128, seeded from DateTime.Now
        /// </summary>
        public Xoroshiro128()
          : this((ulong)DateTime.Now.Ticks)
        {
        }

        /// <summary>
        /// Creates an instance of this RNG using the given state as the starting point
        /// </summary>
        /// <param name="state"></param>
        public Xoroshiro128(State state)
        {
            if (state is null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            this._seed1 = state._seed1;
            this._seed2 = state._seed2;
        }

        /// <summary>
        /// Returns the current machine state so it can be persisted and loaded
        /// </summary>
        /// <returns>The current Machine state</returns>
        public Xoroshiro128.State GetState()
        {
            return new Xoroshiro128.State()
            {
                _seed1 = this._seed1,
                _seed2 = this._seed2
            };
        }

        /// <summary>
        /// Creates a new instance of Xoroshiro128 with a specified seed.
        /// </summary>
        /// <param name="seed">Value to seed this PRNG with.</param>
        public Xoroshiro128(ulong seed)
        {
            long num1;
            ulong num2 = (ulong)(num1 = (long)seed + -7046029254386353131L);
            ulong num3 = (ulong)(((long)num2 ^ (long)(num2 >> 30)) * -4658895280553007687L);
            ulong num4 = (ulong)(((long)num3 ^ (long)(num3 >> 27)) * -7723592293110705685L);
            this._seed1 = num4 ^ num4 >> 31;
            ulong num5 = (ulong)(num1 + -7046029254386353131L);
            ulong num6 = (ulong)(((long)num5 ^ (long)(num5 >> 30)) * -4658895280553007687L);
            ulong num7 = (ulong)(((long)num6 ^ (long)(num6 >> 27)) * -7723592293110705685L);
            this._seed2 = num7 ^ num7 >> 31;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong rotl(ulong x, int k)
        {
            return x << k | x >> 64 - k;
        }

        /// <summary>Get the next ulong for this instance.</summary>
        /// <returns>Next psuedo-random value.</returns>
        public ulong Next()
        {
            ulong seed1 = this._seed1;
            ulong seed2 = this._seed2;
            long num = (long)seed1 + (long)seed2;
            ulong x = seed2 ^ seed1;
            this._seed1 = (ulong)((long)Xoroshiro128.rotl(seed1, 55) ^ (long)x ^ (long)x << 14);
            this._seed2 = Xoroshiro128.rotl(x, 36);
            return (ulong)num;
        }

        /// <summary>Get the next ulong for this instance.</summary>
        /// <returns>Next psuedo-random value.</returns>
        public ulong Next(ulong min, ulong max)
        {
            ulong s0 = _seed1;
            ulong s1 = _seed2;

            ulong result;

            do
            {
                result = s0 + s1;

                s1 ^= s0;
                s0 = rotl(s0, 55) ^ s1 ^ (s1 << 14); // a, b
                s1 = rotl(s1, 36); // c
            } while (result < min || result >= max);

            _seed1 = s0;
            _seed2 = s1;

            return result;
        }

        /// <summary>
        /// Jumps 2 ^ 64 values. This can be used to parallelize operations.
        /// </summary>
        public void Jump()
        {
            ulong num1 = 0;
            ulong num2 = 0;
            for (int index1 = 0; index1 < 2; ++index1)
            {
                for (int index2 = 0; index2 < 64; ++index2)
                {
                    if ((JumpValue[index1] & (ulong)(1L << index2)) > 0UL)
                    {
                        num1 ^= this._seed1;
                        num2 ^= this._seed2;
                    }

                    _ = (long)this.Next();
                }
            }
            this._seed1 = num1;
            this._seed2 = num2;
        }

        /// <summary>
        /// Returns the next double from the sequence generated by taking the state and dividing it by ulong.MaxValue
        /// </summary>
        /// <returns>The next double from the sequence created from the next ulong</returns>
        public double NextDouble()
        {
            return this.Next() / ulong.MaxValue;
        }

        /// <summary>The previous machine state used for saving/loading</summary>
        public class State
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            public ulong _seed1;
            public ulong _seed2;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }
    }
}